using Newtonsoft.Json;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class Notificacao
    {
        [JsonProperty("URL")]
        public string Url { get; set; }
        [JsonProperty("Payload")]
        public dynamic Payload { get; set; }
        [JsonProperty("TipoAutenticacao")]
        public string TipoAutenticacao { get; set; }
    }

}
