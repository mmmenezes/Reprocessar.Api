using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using FluentValidation;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Validators
{
    public class ComprovanteValidator : AbstractValidator<string>
    {
        public ComprovanteValidator(IApiIssuer issuer)
        {

            RuleFor(x => x)
                .NotEmpty()
                .WithMessage(Resources.FriendlyMessages.ErroValidaProtocolo)
                .WithErrorCode(issuer.MakerCode(Issues.vi0032));

            RuleFor(x => x)
            .Must(Shared.IsGuid)
            .WithMessage(Resources.FriendlyMessages.ErroValidaProtocoloInvalido)
            .WithErrorCode(issuer.MakerCode(Issues.vi0026))
            .OverridePropertyName("codigoPagamento");
            
        }

    }
}
