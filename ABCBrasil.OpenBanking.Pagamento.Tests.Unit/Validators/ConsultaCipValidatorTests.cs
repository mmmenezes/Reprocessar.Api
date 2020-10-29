using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.Validators;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Cip;
using FluentAssertions;
using Moq;
using Xunit;

namespace ABCBrasil.OpenBanking.Pagamento.Tests.Unit.Validators
{
    public class ConsultaCipValidatorTests
    {
        readonly Mock<IApiIssuer> _adpiIssuer;

        public ConsultaCipValidatorTests()
        {
            _adpiIssuer = new Mock<IApiIssuer>();
            _adpiIssuer.Setup(x => x.MakerCode(Issues.vi0013)).Returns("0013");
            _adpiIssuer.Setup(x => x.MakerCode(Issues.vi0014)).Returns("0014");
            _adpiIssuer.Setup(x => x.MakerCode(Issues.vi0015)).Returns("0015");
            _adpiIssuer.Setup(x => x.MakerCode(Issues.vi0016)).Returns("0016");
            _adpiIssuer.Setup(x => x.MakerCode(Issues.vi0022)).Returns("0022");
            _adpiIssuer.Setup(x => x.MakerCode(Issues.vi0023)).Returns("0023");
        }

        [Fact(DisplayName = "#01 - Cenário - Deve Retornar False ConsultaCip Invalido")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_False_Cliente_Invalido()
        {
            // Arrange
            var requisicao = Builder.CipBuilder.BuildConsultaCipDefault();
            var validator = new ConsultaCipValidator(_adpiIssuer.Object);

            // Act
            var resultado = validator.Validate(requisicao);

            // Assert
            resultado.IsValid.Should().BeFalse();
            //resultado.Errors.Should().HaveCount(1);
            resultado.Errors.Should().Contain(o => o.ErrorCode.Equals("0013"));
            //resultado.Errors.Should().Contain(o => o.ErrorCode.Equals("0014"));
            //resultado.Errors.Should().Contain(o => o.ErrorCode.Equals("0015"));
            resultado.Errors.Should().Contain(o => o.ErrorCode.Equals("0016"));
            resultado.Errors.Should().Contain(o => o.ErrorMessage.Equals(Core.Resources.FriendlyMessages.ErrorValidaCodigoPagamentoEmpty));
            //resultado.Errors.Should().Contain(o => o.ErrorMessage.Equals(Core.Resources.FriendlyMessages.ErrorValidTricon));
            resultado.Errors.Should().Contain(o => o.ErrorMessage.Equals(Core.Resources.FriendlyMessages.ErroValidaCodigoCliente));
        }


    }
}
