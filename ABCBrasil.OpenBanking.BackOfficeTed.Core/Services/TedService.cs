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
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.ReProcessaTed;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class TedService : ServiceBase, ITedService
    {
        public TedService(IEventoRepository tedRepository, IIBRepository iBRepository, IMapper mapper, IApiIssuer issuer) : base(issuer)
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

            var result = new BuscaTedsResponse
            {
                Teds = TED,

                CSVByte = File.ReadAllBytes(FilePath)
            };
            File.Delete(FilePath);
            return result;

        }

        public ReProcessaTed ProcessaTed(IList<TransferenciasArquivo> SelectedCSV)
        {
            var result = default(ReProcessaTed);
            result = new ReProcessaTed();
            result.quantidadeTotal = SelectedCSV.Count();
            AddTrace("Service Processa Ted");
            try
            {
                AddTrace("Processa ted ", SelectedCSV);
                List<TransferenciaModel> transferencias = new List<TransferenciaModel>();
                int i = 1;
                
                foreach (var item in SelectedCSV)
                {
                    
                    
                    var transferencia = item.transferencia.MapTo<TransferenciaInclui>();
                    transferencia.DcUrlCallBack = item.callback.Url;
                    TedInfo ted = new TedInfo
                    {
                        Cd_Evento_Api = item.root.Codigo.ToString(),
                        Gw_Evento_Api = item.root.Protocolo
                    };
                    transferencia.CdProtocoloApi = item.root.Protocolo;
                    ted.Dc_Payload_Request = JsonSerializer.Serialize(transferencia);
               
                    try
                    {
                        var insereTedsRetorno = _tedRepository.InsereTeds(ted).Result;
                        if (insereTedsRetorno.Count() != 0)
                        {
                        AddTrace($"Falha no Insere Teds. Codigo ted: {ted}", insereTedsRetorno);

                        }

                        var processaTedretorno = _ibRepository.ProcessaTed(transferencia).Result;
                        if (processaTedretorno.Count() != 0)
                        {
                            AddTrace($"Falha no Insere Teds. Codigo ted: {ted}", processaTedretorno);

                        }

                        var processaAtualizaEnvio = _tedRepository.AtualizaEnvio(item.root.Codigo).Result;
                        if (processaTedretorno.Count() != 0)
                        {
                            AddTrace($"Falha no Insere Teds. Codigo ted: {ted}", processaTedretorno);
                        }
                        ted.Status = true;
                    }
                    catch (Exception ex)
                    {

                        AddError(Issues.se3003, Resources.FriendlyMessages.ServiceErrorProcessaPROC, ex);
                        result.quantidadeFalha = i++;
                        ted.Status = false;
                    }


                    
                    result.teds.Add(ted);

                }

            }
            catch (Exception ex)
            {

                AddError(Issues.se3002, Resources.FriendlyMessages.ServiceErrorProcessa, ex);
            }


            return result;


        }


        public List<TransferenciasArquivo> ProcessaArquivo(UploadViewModel file)
        {

            AddTrace("Service Processa arquivo");
            var result = new List<TransferenciasArquivo>();
            try
            {
                using (var stream = file.Teds.OpenReadStream())
                {
                    var csvLines = CsvReader.ReadFromStream(stream);
                    AddTrace("Processa arquivo ", csvLines);
                    foreach (var item in csvLines)
                    {
                        TransferenciasArquivo transArq = new TransferenciasArquivo();

                        var line = Convert.ToString(item).Split(";");
                        transArq.root.Codigo = Int32.Parse(line[0]);
                        transArq.root.Protocolo = line[1].ToString();
                        transArq.transferencia = line[2].FromJson<TransferenciaModel>();
                        var campos = Convert.ToString(line[2]).Split(",");
                        var data = campos[11].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(campos[11].IndexOf(":") - 1);
                        var dataFormat = data.Substring(0, data.IndexOf("T"));
                        transArq.transferencia.DataTransacao = Convert.ToDateTime(dataFormat);
                        var callback = campos[14].ToString().Replace("\"", "").Replace("{", "").Replace("}", "").Substring(campos[11].IndexOf(":")-2 );
                        transArq.callback.Url = callback;
                        result.Add(transArq);

                    }

                }
            }
            catch (Exception ex)
            {
                AddError(Issues.se3001, Resources.FriendlyMessages.ServiceErrorProcessaarquivo, ex);
            }


            return result;
        }
    }
}
