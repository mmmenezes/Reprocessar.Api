using Microsoft.AspNetCore.Http;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Componente
{
    public class LogEventoModel
    {
        public string Payload { get; set; }
        public bool Status { get; set; }
        public HttpRequest Httprequest { get; set; }
        public string ErrorMessage { get; set; }
    }
}
