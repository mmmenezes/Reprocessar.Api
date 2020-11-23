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

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class TedService : ITedService
    {
        public TedService(IEventoRepository tedRepository, IIBRepository iBRepository)
        {
            _tedRepository = tedRepository;
            _ibRepository = iBRepository;
        }

        
        readonly IEventoRepository _tedRepository;
        readonly IIBRepository _ibRepository;
        public BuscaTedsResponse BuscaTeds(BuscaTedRequest tedRequest)
        {

            var teds = _tedRepository.BuscaTeds(tedRequest);
            File.Create("Teds.csv").Close();
            var TED = teds.GetAwaiter().GetResult();
            var csv = new StringBuilder();
            csv.AppendLine(string.Format("{0},{1},{2}", "Codigo Evento", "Protocolo", "Payload"));
            foreach (var item in TED)
            {
                var first = item.Cd_Evento_Api.ToString();
                var second = item.Gw_Evento_Api;
                var third = item.Dc_Payload_Request;
                var newline = string.Format("{0},{1},{2}", first, second, third);
                csv.AppendLine(newline);
            }
            File.WriteAllText("Teds.csv", csv.ToString());
            return new BuscaTedsResponse
            {
                Teds = TED,
                CSVByte = Convert.ToBase64String(File.ReadAllBytes("Teds.csv"))
            };

        }

        public bool ProcessaTed(IList<TransferenciasArquivo> SelectedCSV)
        {

            return false;
            //var selecionadas = JsonSerializer.Deserialize<List<TransferenciasArquivo>>(SelectedCSV).MapTo<List<TransferenciaInclui>>();
            ////SelectedCSV.FromCsv<List<TransferenciasArquivo>>();

            //List<TransferenciaModel> transferencias = new List<TransferenciaModel>();
            //foreach (var item in selecionadas)
            //{
            //    _tedRepository.InsereTeds(item);
            //    var transferencia = JsonSerializer.Deserialize<IncluiTedModel>(item.Dc_Payload_Request).MapTo<TransferenciaInclui>();
            //    _ibRepository.Atualiza(transferencia);
            //    _tedRepository.AtualizaEnvio(Int32.Parse(item.Cd_Evento_Api));


            //}

            return false;


        }

        public List<TransferenciasArquivo> Processaarquivo(UploadViewModel file)
        {
            
            var result = new List<TransferenciasArquivo>();
            
            using (var stream = file.Teds.OpenReadStream())
            {
                var csvLines = CsvReader.ReadFromStream(stream).ToList();

                

                foreach (var item in csvLines)
                {
                    TransferenciasArquivo transArq = new TransferenciasArquivo();
                    DateTime data = DateTime.Now;

                    var line = Convert.ToString(item).Split(",");
                    transArq.root.Codigo = Int32.Parse(line[0]);
                    transArq.root.Protocolo = line[1].ToString();

                    transArq.transferencia.CodCliente = Int32.Parse(line[2].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(line[2].IndexOf(":")-2));
                    transArq.transferencia.AgenciaCliente = line[3].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(line[3].IndexOf(":")-1);
                    transArq.transferencia.ContaCliente = line[4].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(line[4].IndexOf(":") - 1); 
                    transArq.transferencia.TipoContaFavorecido = line[5].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(line[5].IndexOf(":") - 1);
                    transArq.transferencia.BancoFavorecido = line[6].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(line[6].IndexOf(":") - 1);
                    transArq.transferencia.AgenciaFavorecido = line[7].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(line[7].IndexOf(":") - 1);
                    transArq.transferencia.ContaFavorecido = line[8].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(line[8].IndexOf(":") - 1);
                    transArq.transferencia.NumDocumentoFavorecido = line[9].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(line[9].IndexOf(":") - 1);
                    transArq.transferencia.NomeFavorecido = line[10].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(line[10].IndexOf(":") - 1);
                    transArq.transferencia.NumDocumentoFavorecido2 = line[11].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(line[11].IndexOf(":") - 1);
                    transArq.transferencia.NomeFavorecido2 = line[12].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(line[12].IndexOf(":") - 1);
                    transArq.transferencia.DataTransacao = data;
                    transArq.transferencia.Finalidade = line[14].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(line[14].IndexOf(":") - 1);
                    transArq.transferencia.Valor = double.Parse(line[15].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(line[15].IndexOf(":") - 1));

                    result.Add(transArq);

                }
               
            }

            return result;
        }
    }
}
