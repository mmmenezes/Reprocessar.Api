using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CoreCip;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Models.Cip
{
    public class BoletoCipResult : CipBaseResult
    {
        public BoletoPagamentoCompleto BoletoPagamentoCompleto { get; set; }
        public string ConsultaCIP { get; set; }
    }
}
