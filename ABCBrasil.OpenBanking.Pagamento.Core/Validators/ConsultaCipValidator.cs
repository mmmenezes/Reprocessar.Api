using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Cip;
using FluentValidation;
namespace ABCBrasil.OpenBanking.Pagamento.Core.Validators
{
    public class ConsultaCipValidator : AbstractValidator<ConsultaCipRequest>
    {
        public ConsultaCipValidator(IApiIssuer issuer)
        {
            RuleFor(x => x.codigoCliente)
            .NotEmpty()
            .WithMessage(Resources.FriendlyMessages.ErroValidaCodigoCliente)
            .WithErrorCode(issuer.MakerCode(Issues.vi0016));

            RuleFor(x => x.codigoPagamento)
                .NotEmpty()
                .WithMessage(Resources.FriendlyMessages.ErrorValidaCodigoPagamentoEmpty)
                .WithErrorCode(issuer.MakerCode(Issues.vi0013))
                .MinimumLength(44)
                .WithMessage(Resources.FriendlyMessages.ErrorValidaCodigoPagamentoMin44)
                .WithErrorCode(issuer.MakerCode(Issues.vi0014))
                .MaximumLength(47)
                .WithMessage(Resources.FriendlyMessages.ErrorValidaCodigoPagamentoMax47)
                .WithErrorCode(issuer.MakerCode(Issues.vi0015));

            When(payload => !string.IsNullOrEmpty(payload.codigoPagamento) && payload.codigoPagamento.Trim().Length > 43 && payload.codigoPagamento.Trim().Length < 48, () =>
            {
                RuleFor(x => x.codigoPagamento)
                .Must(Common.Shared.ValidaCodigoPagamento)
                .WithMessage(Resources.FriendlyMessages.ErrorValidaCodigoPagamento)
                .WithErrorCode(issuer.MakerCode(Issues.vi0022))
                .OverridePropertyName("codigoPagamento")

                .Must(Common.Shared.ValidaTricon)
                .WithMessage(Resources.FriendlyMessages.ErrorValidTricon)
                .WithErrorCode(issuer.MakerCode(Issues.vi0023))
                .OverridePropertyName("codigoPagamento");

            });
        }

    }
}
