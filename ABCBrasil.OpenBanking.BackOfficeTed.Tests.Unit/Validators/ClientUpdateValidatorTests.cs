using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Validators;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Commands;
using FluentAssertions;
using Moq;
using Xunit;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Tests.Unit.Validators
{
    public class ClientUpdateValidatorTests
    {
        readonly Mock<IApiIssuer> _adpiIssuer;

        public ClientUpdateValidatorTests()
        {
            _adpiIssuer = new Mock<IApiIssuer>();
            _adpiIssuer.Setup(x => x.MakerCode(Issues.vi0003)).Returns("0003");
            _adpiIssuer.Setup(x => x.MakerCode(Issues.vi0004)).Returns("0004");
        }

        [Fact(DisplayName = "#01 - Cenário - Deve Retornar False Cliente Invalido")]
        [Trait("Category", "Fail")]
        public void Deve_Retornar_False_Cliente_Invalido()
        {
            // Arrange
            var requesicao = ClientBuilder.BuildClientDefault();
            var validator = new ClientUpdateValidator(_adpiIssuer.Object);

            // Act
            var resultado = validator.Validate(requesicao);

            // Assert
            resultado.IsValid.Should().BeFalse();
            resultado.Errors.Should().HaveCount(1);

            resultado.Errors.Should().Contain(o => o.ErrorCode.Equals("0003"));
            resultado.Errors.Should().Contain(o => o.ErrorMessage.Equals(Core.Resources.FriendlyMessages.ErrorValidNameEmpty));
        }

        internal static class ClientBuilder
        {
            internal static UpdateClientCommand BuildClientDefault()
            {
                return new UpdateClientCommand
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
