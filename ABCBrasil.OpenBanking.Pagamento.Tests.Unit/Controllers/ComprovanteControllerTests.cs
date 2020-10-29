using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.OpenBanking.Pagamento.Api.Controllers;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Comprovante;
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
    public class ComprovanteControllerTests
    {
        readonly IWindsorContainer container;
        readonly ComprovantesController controller;

        public ComprovanteControllerTests()
        {
            container = new WindsorContainer();
            container.Install(new BaseInstaller<ComprovantesController>());
            //
            Builder.ArrangeServiceIssuer(container, Issues.vi0031);
            controller = container.Resolve<ComprovantesController>();
        }

        [Fact(DisplayName = "ComprovanteControllerTests - #01 - CENÁRIO - EXCEÇÃO")]
        [Trait("Controller", "Fail")]
        public async Task Retornar_Excecao_Get()
        {
            // Arrange
            container.Resolve<Mock<IComprovanteService>>()
                .Setup(x => x.ObterComprovante(It.IsAny<string>()))
                .Throws(new Exception());

            // Act
            var actionResult = await controller.GetComprovante(Builder.ComprovanteBuilder.buildComprovante());

            // Assert
            var requestResult = actionResult.Should().BeOfType<ObjectResult>().Subject;
            requestResult.Should().NotBeNull();
            requestResult.StatusCode.Should().Equals(StatusCodes.Status400BadRequest);

            var apiResult = requestResult.Value.Should().BeOfType<ApiResult<object>>().Subject;

            apiResult.Should().NotBeNull();
        }

        [Fact(DisplayName = "ComprovanteControllerTests - [Get] - #02 - CENÁRIO - SUCESSO")]
        [Trait("Controller - Get Payload Response Ok", "SUCCESS")]
        public async Task Retornar_Sucesso_Get()
        {
            // Arrange
            container.Resolve<Mock<IComprovanteService>>()
                .Setup(x => x.ObterComprovante(It.IsAny<string>()))
                .ReturnsAsync(new ComprovanteResponse() { });

            // Act
            var actionResult = await controller.GetComprovante(Builder.ComprovanteBuilder.buildComprovante());

            // Assert
            var requestResult = actionResult.Should().BeOfType<ObjectResult>().Subject;
            requestResult.Should().NotBeNull();
            requestResult.StatusCode.Should().Equals(StatusCodes.Status200OK);

            var apiResult = requestResult.Value.Should().BeOfType<ApiResult<object>>().Subject;

            apiResult.Should().NotBeNull();
        }
    }
}
