namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.Calculo
{
    public class CalculoResponse
    {
        public decimal ValorTituloOriginal { get; set; }
        public decimal ValorTotalCobrar { get; set; }
        public decimal ValorDescontoCalculado { get; set; }
        public decimal ValorJurosCalculado { get; set; }
        public decimal ValorMultaCalculado { get; set; }
        public decimal ValorAbatimento { get; set; }
    }
}
