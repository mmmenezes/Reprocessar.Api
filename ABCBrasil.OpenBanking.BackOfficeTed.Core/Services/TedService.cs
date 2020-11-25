using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Mappings;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using Csv;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;


namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class TedService : ITedService
    {
        public TedService(IEventoRepository tedRepository, IIBRepository iBRepository, IMapper mapper)
        {
            _tedRepository = tedRepository;
            _ibRepository = iBRepository;
        }
        public const string FilePath = "Teds.csv";
        
        readonly IEventoRepository _tedRepository;
        readonly IIBRepository _ibRepository;
        public BuscaTedsResponse BuscaTeds(BuscaTedRequest tedRequest)
        {

            var teds = _tedRepository.BuscaTeds(tedRequest);
            File.Create(FilePath).Close();
            var TED = teds.GetAwaiter().GetResult();
            var csv = new StringBuilder();
            csv.AppendLine(string.Format("{0};{1};{2}", "Codigo Evento", "Protocolo", "Payload"));
            foreach (var item in TED)
            {
                var first = item.Cd_Evento_Api.ToString();
                var second = item.Gw_Evento_Api;
                var third = item.Dc_Payload_Request;
                var newline = string.Format("{0};{1};{2}", first, second, third);
                csv.AppendLine(newline);
            }
            File.WriteAllText(FilePath, csv.ToString());
            File.Delete(FilePath);
            return new BuscaTedsResponse
            {
                Teds = TED,
                CSVByteString = Convert.ToBase64String(File.ReadAllBytes(FilePath)),
                CSVByte = File.ReadAllBytes(FilePath)
            };

        }

        public bool ProcessaTed(IList<TransferenciasArquivo> SelectedCSV)
        {

            //return false;
            //var selecionadas = JsonSerializer.Deserialize<List<TransferenciasArquivo>>(SelectedCSV).MapTo<List<TransferenciaInclui>>();
            

            List<TransferenciaModel> transferencias = new List<TransferenciaModel>();
            foreach (var item in SelectedCSV)
            {
                var transferencia = JsonSerializer.Deserialize<TransferenciaModel>(item.transferencia.ToJson()).MapTo<TransferenciaInclui>();
                TedInfo ted = new TedInfo();
                ted.Cd_Evento_Api = item.root.Codigo.ToString();
                ted.Gw_Evento_Api = item.root.Protocolo;
                ted.Dc_Payload_Request = JsonSerializer.Serialize(transferencia);

                var teste = _tedRepository.InsereTeds(ted);
                var teste2 =_ibRepository.Atualiza(transferencia);
                var teste3 = _tedRepository.AtualizaEnvio(item.root.Codigo);


            }

            return false;


        }

        public List<TransferenciasArquivo> Processaarquivo(UploadViewModel file)
        {

            var result = new List<TransferenciasArquivo>();
            
            using (var stream = file.Teds.OpenReadStream())
            {
                var csvLines = CsvReader.ReadFromStream(stream);
                
                foreach (var item in csvLines)
                {
                    TransferenciasArquivo transArq = new TransferenciasArquivo();
                   
                                        
                    var line = Convert.ToString(item).Split(";");
                    transArq.root.Codigo = Int32.Parse(line[0]);
                    transArq.root.Protocolo = line[1].ToString();
                    transArq.transferencia = line[2].FromJson<TransferenciaModel>();
                    //var campos = Convert.ToString(line[2]).Split(",");
                    //var data = campos[11].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(campos[11].IndexOf(":") - 1);
                    //var dataFormat = data.Substring(0,data.IndexOf("T"));

                    //transArq.transferencia.DataTransacao = DateTime.Parse(dataFormat);

                    result.Add(transArq);

                }
          
            }

            return result;
        }
    }
}
