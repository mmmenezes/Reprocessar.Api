using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Callback;
using FluentValidation;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Validators.Callback
{
    public class CallbackTibcoValidator : AbstractValidator<CallbackTibcoRequest>
    {
        public CallbackTibcoValidator(IApiIssuer issuer)
        {
        }
    }
}