using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CallbackCore
{
    public class CallbackCoreBaseRequest
    {
        public CallbackCoreBaseRequest()
        {
            Infos = new List<CallbackCoreInfos>();
            Errors = new List<CallbackCoreErrors>();
        }
        public bool Status { get; set; }
        public string Name { get; set; }
        public string EnvironmentName { get; set; }
        public DateTime Date { get; set; }
        public CallbackCoreDataRequest Data { get; set; }
        public List<CallbackCoreInfos> Infos { get; set; }
        public List<CallbackCoreErrors> Errors { get; set; }
    }
    public class CallbackCoreInfos
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
    public class CallbackCoreErrors
    {
        public string Code { get; set; }
        public string Message { get; set; }
    }
    public class CallbackCoreDataRequest
    {
        public bool Status { get; set; }
        [JsonProperty("Cd_status")]
        public int CdStatus { get; set; }
        public string Idenficador { get; set; }
        public string Autenticacao { get; set; }
        public DateTime DataProcessamento { get; set; }
        public string MensagemAmigavel { get; set; }
        public string MensagemInterno { get; set; }
    }
}
