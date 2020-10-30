using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Commands;
using FluentValidation;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Validators
{
    public class ClientRegisterValidator : AbstractValidator<RegisterClientCommand>
    {
        public ClientRegisterValidator(IApiIssuer issuer)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(Resources.FriendlyMessages.ErrorValidNameEmpty)
                .WithErrorCode(issuer.MakerCode(Issues.vi0001))
                .MinimumLength(10)
                .WithMessage(Resources.FriendlyMessages.ErrorValidName10)
                .WithErrorCode(issuer.MakerCode(Issues.vi0002));

            When(payload => payload.TypeDocument.ToString().ToUpper() == "CPF", () =>
            {
                RuleFor(x => x.Document)
                .Must(Common.Shared.ValidaCnpjCpf)
                .WithMessage(Resources.FriendlyMessages.InvalidCpf)
                .OverridePropertyName("document");
            });

        }
    }
}
