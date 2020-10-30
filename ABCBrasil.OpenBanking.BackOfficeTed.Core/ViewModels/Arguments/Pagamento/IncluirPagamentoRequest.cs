using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Pagamento
{
    public class IncluirPagamentoRequest
    {
        public int CodigoCliente { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public DateTime DataPagamento { get; set; }
        public string IdentificacaoPagamento { get; set; }
        public string CodigoPagamento { get; set; }
        public decimal ValorPagamento { get; set; }
        public string UrlNotificacao { get; set; }
    }
}
