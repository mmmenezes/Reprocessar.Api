using System;
using System.Collections.Generic;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.Tibco
{
    public class IncluirPagamentoTibcoRequest
    {
        public int Crc { get; set; }
        public string ColigadaDebito { get; set; }
        public string AgenciaDebito { get; set; }
        public string ContaDebito { get; set; }
        public int Canal { get; set; }
        public DateTime DataRequisicao { get; set; }
        public bool Reagendado { get; set; }
        public string Protocolo { get; set; }
        public string IdPagamento { get; set; }
        public int PagamentoSituacao { get; set; }
        public string UrlCallbackCliente { get; set; }
        public string ApiKeyNameCallbackCliente { get; set; }
        public string ApiKeyValueCallbackCliente { get; set; }
        public string LoginOpenBanking { get; set; }
        public List<BoletoTibcoRequest> Boletos { get; set; }
        public List<TrinConTibcoRequest> Trincons { get; set; }
        public CallbackOrigemTibcoRequest CallBackOrigem { get; set; }
    }

    public class CallbackOrigemTibcoRequest
    {
        public string Url { get; set; }
        public string ApiKeyName { get; set; }
        public string ApiKeyValue { get; set; }
    }

    public class BoletoTibcoRequest
    {
        public int TipoPagamento { get; set; }
        public string CodigoBarras { get; set; }
        public string LinhaDigitavel { get; set; }
        public string CodigoEntidade { get; set; }
        public string NomeEntidade { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public float ValorTitulo { get; set; }
        public float ValorPagamento { get; set; }
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
        public string IdentificacaoCanal { get; set; }
    }

    public class TrinConTibcoRequest
    {
        public int TipoPagamento { get; set; }
        public string NomePagador { get; set; }
        public string CpfCnpjPagador { get; set; }
        public string CodigoBarras { get; set; }
        public string LinhaDigitavel { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime DataPagamento { get; set; }
        public decimal ValorPagamento { get; set; }
        public string IdentificacaoCanal { get; set; }
    }
}
