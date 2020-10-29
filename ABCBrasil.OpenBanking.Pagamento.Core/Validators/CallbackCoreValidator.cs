using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CallbackCore;
using FluentValidation;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Validators
{
    public class CallbackCoreValidator : AbstractValidator<CallbackCoreDataRequest>
    {
        public CallbackCoreValidator(IApiIssuer issuer)
        {
        }
    }
}
