using ABCBrasil.OpenBanking.Pagamento.Core.Common;
using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Pagamento;
using FluentValidation;
using System;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Validators
{
    public class SituacaoPagamentoProtocoloValidator : AbstractValidator<SituacaoPagamentoProtocoloRequest>
    {
        public SituacaoPagamentoProtocoloValidator(IApiIssuer issuer)
        {

            RuleFor(x => x.protocolo)
                .NotEmpty()
                .WithMessage(Resources.FriendlyMessages.ErroValidaProtocolo)
                .WithErrorCode(issuer.MakerCode(Issues.vi0030));

            RuleFor(x => x.protocolo)
            .Must(Shared.IsGuid)
            .WithMessage(Resources.FriendlyMessages.ErroValidaProtocoloInvalido)
            .WithErrorCode(issuer.MakerCode(Issues.vi0025))
            .OverridePropertyName("codigoPagamento");
            
        }

    }
    public class SituacaoPagamentoIdentificadorValidator : AbstractValidator<SituacaoPagamentoIdentificadorRequest>
    {
        public SituacaoPagamentoIdentificadorValidator(IApiIssuer issuer)
        {

            RuleFor(x => x.identificadorPagamento)
                .NotEmpty()
                .WithMessage(Resources.FriendlyMessages.ErroValidaIdentificadorPagamento)
                .WithErrorCode(issuer.MakerCode(Issues.vi0031));
        }

    }
}
