using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Callback;
using FluentValidation;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Validators.Callback
{
    public class CallbackTibcoValidator : AbstractValidator<CallbackTibcoRequest>
    {
        public CallbackTibcoValidator(IApiIssuer issuer)
        {
        }
    }
}