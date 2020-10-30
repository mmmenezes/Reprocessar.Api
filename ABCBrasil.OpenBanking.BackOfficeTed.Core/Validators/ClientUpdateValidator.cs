using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Commands;
using FluentValidation;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Validators
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
