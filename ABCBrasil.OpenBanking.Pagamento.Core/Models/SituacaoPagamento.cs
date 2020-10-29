using System;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Models
{
    public class SituacaoPagamento
    {
        public Guid Protocolo { get; set; }
        public int Situacao { get; set; }
        public int Retorno { get; set; }
        public string Mensagem { get; set; }
    }
}
