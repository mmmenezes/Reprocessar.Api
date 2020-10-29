using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.OpenBanking.Pagamento.Api.Controllers;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Commands;
using Castle.Windsor;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ABCBrasil.OpenBanking.Pagamento.Tests.Unit.Controllers
{
    public class ClientsControllerTests
    {
        readonly IWindsorContainer container;
        readonly ClientsController controller;
        public ClientsControllerTests()
        {
            container = new WindsorContainer();
            container.Install(new BaseInstaller<ClientsController>());
            
            Builder.ArrangeServiceIssuer(container, Issues.se3002);

            controller = container.Resolve<ClientsController>();
        }

        [Fact(DisplayName = "#01 - Cenário: Erro Genérico")]
        [Trait("Category", "Fail")]
        public async Task Retornar_Erro_Generico_Quando_Falhar()
        {
            // Arrange
            container.Resolve<Mock<IClientService>>()
                .Setup(x => x.CreateAsync(It.IsAny<RegisterClientCommand>()))
                .Throws(new Exception());
            
            // Act
            var actionResult = await controller.Post(ClientBuilder.BuildClientDefault());

            // Assert
            var requestResult = actionResult.Should().BeOfType<ObjectResult>().Subject;
            requestResult.Should().NotBeNull();
            requestResult.StatusCode.Should().Equals(StatusCodes.Status400BadRequest);

            var apiResult = requestResult.Value.Should().BeOfType<ApiResult<Core.Models.ClientViewModel>>().Subject;

            apiResult.Should().NotBeNull();
            //apiResult.Status.Should().BeFalse();
            //apiResult.Errors.Should().Contain(o => o.Code.Equals("2001") && o.Message.Equals("Ocorreu uma falha na sua solicitação. Por favor tente novamente."));
        }
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