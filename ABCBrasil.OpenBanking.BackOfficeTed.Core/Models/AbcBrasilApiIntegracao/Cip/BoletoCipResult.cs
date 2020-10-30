using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.CoreCip;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Cip
{
    public class BoletoCipResult : CipBaseResult
    {
        public BoletoPagamentoCompleto BoletoPagamentoCompleto { get; set; }
        public string ConsultaCIP { get; set; }
    }
}
