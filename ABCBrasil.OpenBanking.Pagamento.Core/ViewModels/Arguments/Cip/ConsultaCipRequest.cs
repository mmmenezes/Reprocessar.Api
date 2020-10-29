using Microsoft.AspNetCore.Mvc;

namespace ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Cip
{
    public class ConsultaCipRequest
    {
        [FromRoute]
        public int codigoCliente { get; set; }
        [FromRoute]
        public string codigoPagamento { get; set; }
    }
}
