using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Pagamento;
using FluentValidation;
using System;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Validators
{
    public class IncluirPagamentoRequestValidator : AbstractValidator<IncluirPagamentoRequest>
    {
        public IncluirPagamentoRequestValidator(IApiIssuer issuer)
        {
            RuleFor(x => x.CodigoCliente)
                .NotEmpty()
                .WithMessage(Resources.FriendlyMessages.ErroValidaCodigoCliente)
                .WithErrorCode(issuer.MakerCode(Issues.vi0005));

            RuleFor(x => x.Agencia)
                .NotEmpty()
                .WithMessage(Resources.FriendlyMessages.ErroValidaAgencia)
                .WithErrorCode(issuer.MakerCode(Issues.vi0006));

            RuleFor(x => x.Conta)
                .NotEmpty()
                .WithMessage(Resources.FriendlyMessages.ErroValidaConta)
                .WithErrorCode(issuer.MakerCode(Issues.vi0007));

            RuleFor(x => x.UrlNotificacao)
                .NotEmpty()
                .WithMessage(Resources.FriendlyMessages.ErroValidaUrlNotificacao)
                .WithErrorCode(issuer.MakerCode(Issues.vi0008));

            RuleFor(x => x.DataPagamento)
                .NotEmpty()
                .WithMessage(Resources.FriendlyMessages.ErroValidaDataPagamento)
                .WithErrorCode(issuer.MakerCode(Issues.vi0009))
                .Must(x => x.Date >= DateTime.Now.Date)
                .WithMessage(Resources.FriendlyMessages.ErroValidaDataPagamentoInferior)
                .WithErrorCode(issuer.MakerCode(Issues.vi0022));

            RuleFor(x => x.IdentificacaoPagamento)
                .NotEmpty()
                .WithMessage(Resources.FriendlyMessages.ErroValidaIdPagamento)
                .WithErrorCode(issuer.MakerCode(Issues.vi0010));

            RuleFor(x => x.CodigoPagamento)
               .NotEmpty()
               .WithMessage(Resources.FriendlyMessages.ErrorValidaCodigoPagamentoEmpty)
               .WithErrorCode(issuer.MakerCode(Issues.vi0018))
               .MinimumLength(44)
               .WithMessage(Resources.FriendlyMessages.ErrorValidaCodigoPagamentoMin44)
               .WithErrorCode(issuer.MakerCode(Issues.vi0019))
               .MaximumLength(47)
               .WithMessage(Resources.FriendlyMessages.ErrorValidaCodigoPagamentoMax47)
               .WithErrorCode(issuer.MakerCode(Issues.vi0020));

            When(payload => payload.CodigoPagamento != null && payload.CodigoPagamento.Trim().Length > 43 && payload.CodigoPagamento.Trim().Length < 48, () =>
            {
                RuleFor(x => x.CodigoPagamento)

                .Must(x => !x.StartsWith('8'))
                .When(Common.Shared.ValidaValorTriCon)
                .WithMessage(Resources.FriendlyMessages.ErroValidaValorTriCon)
                .WithErrorCode(issuer.MakerCode(Issues.vi0021))
                .OverridePropertyName("codigoPagamento")

                .Must(Common.Shared.ValidaCodigoPagamento)
                .WithMessage(Resources.FriendlyMessages.ErrorValidaCodigoPagamento)
                .WithErrorCode(issuer.MakerCode(Issues.vi0017))
                .OverridePropertyName("codigoPagamento");
            });

            RuleFor(v => v.ValorPagamento)
                .Must(v => v > 0)
                .WithMessage(Resources.FriendlyMessages.ErroValidaValorZeroPagamento)
                .WithErrorCode(issuer.MakerCode(Issues.vi0025));
        }
    }
}
