using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository
{
    public class PagamentoExisteRequest : IEquatable<PagamentoExisteResponse>
    {
        public int CodigoCliente { get; set; }
        public string IdentificacaoPagamento { get; set; }
        public string Conta { get; set; }
        public decimal ValorPagamento { get; set; }
        public string CodigoPagamento { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as PagamentoExisteResponse);
        }

        public bool Equals(PagamentoExisteResponse other)
        {
            if (other == null)
                return false;

            return CodigoCliente.Equals(other.CodigoCliente) &&
                IdentificacaoPagamento.Equals(other.IdentificacaoPagamento) &&
                Conta.Equals(other.Conta) &&
                ValorPagamento.Equals(other.ValorPagamento) &&
                CodigoPagamento.Equals(other.CodigoPagamento);
        }
    }
}
