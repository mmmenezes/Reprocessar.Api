using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Controllers;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Pagamento;
using Castle.Windsor;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Tests.Unit.Controllers
{
    public class PagamentoControllerTests
    {
        readonly IWindsorContainer container;
        readonly PagamentosController controller;

        public PagamentoControllerTests()
        {
            container = new WindsorContainer();
            container.Install(new BaseInstaller<PagamentosController>());
            //
            Builder.ArrangeServiceIssuer(container, Issues.vi0031);
            controller = container.Resolve<PagamentosController>();
        }

        [Fact(DisplayName = "PagamentoControllerTests - #01 - CENÁRIO - EXCEÇÃO")]
        [Trait("Controller", "Fail")]
        public async Task Retornar_Excecao_Get()
        {
            // Arrange
            container.Resolve<Mock<IPagamentoService>>()
                .Setup(x => x.ObterSituacaoIdentificador(It.IsAny<SituacaoPagamentoIdentificadorRequest>()))
                .Throws(new Exception());

            // Act
            var actionResult = await controller.GetSituacaoIdentificador(Builder.PagamentoBuilder.BuildConsultaIdentificadorPagamento());

            // Assert
            var requestResult = actionResult.Should().BeOfType<ObjectResult>().Subject;
            requestResult.Should().NotBeNull();
            requestResult.StatusCode.Should().Equals(StatusCodes.Status400BadRequest);

            var apiResult = requestResult.Value.Should().BeOfType<ApiResult<object>>().Subject;

            apiResult.Should().NotBeNull();
        }

        [Fact(DisplayName = "PagamentoControllerTests - [Get] - #02 - CENÁRIO - SUCESSO")]
        [Trait("Controller - Get Payload Response Ok", "SUCCESS")]
        public async Task Retornar_Sucesso_Get()
        {
            // Arrange
            container.Resolve<Mock<IPagamentoService>>()
                .Setup(x => x.ObterSituacaoIdentificador(It.IsAny<SituacaoPagamentoIdentificadorRequest>()))
                .ReturnsAsync(new SituacaoPagamentoResponse() { });

            // Act
            var actionResult = await controller.GetSituacaoIdentificador(Builder.PagamentoBuilder.BuildConsultaIdentificadorPagamento());

            // Assert
            var requestResult = actionResult.Should().BeOfType<ObjectResult>().Subject;
            requestResult.Should().NotBeNull();
            requestResult.StatusCode.Should().Equals(StatusCodes.Status200OK);

            var apiResult = requestResult.Value.Should().BeOfType<ApiResult<object>>().Subject;

            apiResult.Should().NotBeNull();
        }
    }
}
