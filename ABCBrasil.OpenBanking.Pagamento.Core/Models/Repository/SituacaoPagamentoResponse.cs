using Newtonsoft.Json;
using System;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.Repository
{
    public class SituacaoProtocoloPagamentoResponse
    { 
        public string Protocolo { get; set; }
        public string Arquivo { get; set; }
        public string CodigoBarras { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string IdentificacaoPagamento { get; set; }
        public int? Situacao { get; set; }
        public string Descricao { get; set; }
        public string DescricaoLog { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public decimal? Valor { get; set; }
    }
}
