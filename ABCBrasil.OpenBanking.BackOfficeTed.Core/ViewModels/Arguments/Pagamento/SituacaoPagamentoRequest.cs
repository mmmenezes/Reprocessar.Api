using Microsoft.AspNetCore.Mvc;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Pagamento
{
    public class SituacaoPagamentoProtocoloRequest
    {
        [FromRoute]
        public string protocolo { get; set; }
    }
    public class SituacaoPagamentoIdentificadorRequest
    {
        [FromRoute]
        public string identificadorPagamento { get; set; }
    }
}
