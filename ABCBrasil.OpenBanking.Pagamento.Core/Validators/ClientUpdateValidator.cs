using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Commands;
using FluentValidation;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Validators
{
    public class ClientUpdateValidator : AbstractValidator<UpdateClientCommand>
    {
        public ClientUpdateValidator(IApiIssuer issuer)
        {

            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(Resources.FriendlyMessages.ErrorValidNameEmpty)
                .WithErrorCode(issuer.MakerCode(Issues.vi0003))
                .MinimumLength(10)
                .WithMessage(Resources.FriendlyMessages.ErrorValidName10)
                .WithErrorCode(issuer.MakerCode(Issues.vi0004));

        }
    }
}
