using System;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.Calculo
{
    public class CalculaTituloRequest
    {
        public string CodigoBarra { get; set; }
        public Guid Protocolo { get; set; }
        public int CodCliente { get; set; }
        public DateTime Data { get; set; }
        public string AgenciaCliente { get; set; }
        public string ContaCliente { get; set; }
        public string IdentificacaoPagamento { get; set; }
        public int CodigoCanal { get; set; }
    }
}
