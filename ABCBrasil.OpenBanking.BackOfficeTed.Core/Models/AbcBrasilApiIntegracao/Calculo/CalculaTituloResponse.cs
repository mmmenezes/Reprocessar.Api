using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Calculo
{
    public class CalculaTituloResponse
    {
        public bool Sucesso { get; set; }
        public string? MensagemErro { get; set; }
        public string CodigoBarra { get; set; }
        public string CodigoCliente { get; set; }
        public string Protocolo { get; set; }
        public string UrlCliente { get; set; }
        public DateTime Data { get; set; }
        public decimal? Valor { get; set; }
        public string AgenciaCliente { get; set; }
        public string ContaCliente { get; set; }
        public string IdentificacaoPagamento { get; set; }
        public CalculoValorCobrarReturn? CalculoValorCobrarReturn { get; set; }
        public RespostaConsultaCip RespostaConsultaCip { get; set; }
        public DateTime DataProvavelpagamento { get; set; }
    }

    public class RespostaConsultaCip
    {
        public int CodErro { get; set; }
        public string? XmlR1 { get; set; }
        public string? DescErro { get; set; }
    }

    public class CalculoValorCobrarReturn
    {
        public decimal ValorTituloOriginal { get; set; }
        public decimal ValorTotalCobrar { get; set; }
        public decimal ValorDescontoCalculado { get; set; }
        public decimal ValorJurosCalculado { get; set; }
        public decimal ValorMultaCalculado { get; set; }
        public decimal ValorAbatimento { get; set; }
        public string DataOperacao { get; set; }
    }
}
