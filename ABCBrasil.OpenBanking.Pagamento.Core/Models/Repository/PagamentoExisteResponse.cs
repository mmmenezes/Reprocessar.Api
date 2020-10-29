using System;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.Repository
{
    public class PagamentoExisteResponse
    {
        public int CodigoCliente { get; set; }
        public string IdentificacaoPagamento { get; set; }
        public string Conta { get; set; }
        public decimal ValorPagamento { get; set; }
        public string CodigoPagamento { get; set; }
        public Guid Protocolo { get; set; }
    }
}
