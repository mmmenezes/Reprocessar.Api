using ABCBrasil.OpenBanking.BackOfficeTed.Core.Enum;
using System;
using System.Collections.Generic;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models
{
    public class Pagamento
    {
        public Pagamento()
        {
            Boleto = new List<Boleto>();
            TriCon = new List<TriCon>();
            CallBackOrigem = new CallbackOrigem();
        }

        public Guid Protocolo { get; set; }
        public int CodigoCliente { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public DateTime DataPagamento { get; set; }
        public string IdentificacaoPagamento { get; set; }
        public string CodigoPagamento { get; set; }
        public decimal ValorPagamento { get; set; }
        public TipoPagamentoValor TipoPagamento { get; set; }
        public bool HorarioNegado { get; set; }
        public string MensagemHorarioNegado { get; set; }
        public string UrlNotificacao { get; set; }
        public string ApiKeyNotificacao { get; set; }
        public string ApivalueNotificacao { get; set; }
        public string LoginOpenBanking { get; set; }
        public string ColigadaDebito { get; set; }
        public string Canal { get; set; }
        public DateTime DataRequisicao { get; set; }
        public bool Reagendado { get; set; }
        public int PagamentoSituacao { get; set; }

        public List<Boleto> Boleto { get; set; }
        public List<TriCon> TriCon { get; set; }
        public CallbackOrigem CallBackOrigem { get; set; }
    }

    public class Boleto
    {
        public int TipoPagamento { get; set; }
        public string CodigoBarras { get; set; }
        public string LinhaDigitavel { get; set; }
        public string CodigoEntidade { get; set; }
        public string NomeEntidade { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorTitulo { get; set; }
        public decimal ValorPagamento { get; set; }
        public string NomeBeneficiario { get; set; }
        public string CpfCnpjBeneficiario { get; set; }
        public string NomeBeneficiarioFinal { get; set; }
        public string CpfCnpjBeneficiarioFinal { get; set; }
        public string NomePagador { get; set; }
        public string CpfCnpjPagador { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorAbatimento { get; set; }
        public decimal ValorJuros { get; set; }
        public decimal ValorMulta { get; set; }
        public string NumeroDocumento { get; set; }
        public Guid IdentificacaoCanal { get; set; } //Protocolo
    }
    public class TriCon
    {
        public int TipoPagamento { get; set; }
        public string NomePagador { get; set; }
        public string CpfCnpjPagador { get; set; }
        public string CodigoBarras { get; set; }
        public string LinhaDigitavel { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPagamento { get; set; }
        public Guid IdentificacaoCanal { get; set; }
    }
    public class CallbackOrigem
    {
        public string Url { get; set; }
        public string ApiKeyName { get; set; }
        public string ApiKeyValue { get; set; }
    }
}
