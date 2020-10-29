using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.Validators;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Commands;
using FluentAssertions;
using Moq;
using Xunit;

namespace ABCBrasil.OpenBanking.Pagamento.Tests.Unit.Validators
{
    public class ClientRegisterValidatorTests
    {
        readonly Mock<IApiIssuer> _adpiIssuer;

        public ClientRegisterValidatorTests()
        {
            _adpiIssuer = new Mock<IApiIssuer>();
            _adpiIssuer.Setup(x => x.MakerCode(Issues.vi0001)).Returns("0001");
            _adpiIssuer.Setup(x => x.MakerCode(Issues.vi0002)).Returns("0002");
        }

        [Fact(DisplayName = "#01 - Cenário - Deve Retornar False Cliente Invalido")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_False_Cliente_Invalido()
        {
            // Arrange
            var requesicao = ClientBuilder.BuildClientDefault();
            var validator = new ClientRegisterValidator(_adpiIssuer.Object);

            // Act
            var resultado = validator.Validate(requesicao);

            // Assert
            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().HaveCount(1);
            resultado.Errors.Should().Contain(o => o.ErrorCode.Equals("0001"));
            resultado.Errors.Should().Contain(o => o.ErrorMessage.Equals(Core.Resources.FriendlyMessages.ErrorValidNameEmpty));
        }

        internal static class ClientBuilder
        {
            internal static RegisterClientCommand BuildClientDefault()
            {
                return new RegisterClientCommand
                {
                    Name = default,
                    Contact = default,
                    Document = default,
                    Genre = default,
                    MaritalStatus = default,
                    TypeDocument = default
                };
            }
        }

    }
}
