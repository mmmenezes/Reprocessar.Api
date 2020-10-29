using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.OpenBanking.Pagamento.Api.Controllers;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Cip;
using Castle.Windsor;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using Xunit;
using static ABCBrasil.OpenBanking.Pagamento.Tests.Unit.Builder;

namespace ABCBrasil.OpenBanking.Pagamento.Tests.Unit.Controllers
{
    public class CipControllerTests
    {
        readonly IWindsorContainer container;
        readonly CipController controller;

        public CipControllerTests()
        {
            container = new WindsorContainer();
            container.Install(new BaseInstaller<CipController>());

            Builder.ArrangeServiceIssuer(container, Issues.vi0016);
            Builder.ArrangeServiceIssuer(container, Issues.vi0013);
            Builder.ArrangeServiceIssuer(container, Issues.vi0014);
            Builder.ArrangeServiceIssuer(container, Issues.vi0015);
            Builder.ArrangeServiceIssuer(container, Issues.vi0022);
            Builder.ArrangeServiceIssuer(container, Issues.vi0023);

            controller = container.Resolve<CipController>();
        }

        [Fact(DisplayName = "CipControllerTests - #01 - CENÁRIO - EXCEÇÃO")]
        [Trait("Controller", "Fail")]
        public async Task Retornar_Excecao_Get()
        {
            // Arrange
            container.Resolve<Mock<ICipService>>()
                .Setup(x => x.ObterBoletoAsync(It.IsAny<ConsultaCipRequest>()))
                .Throws(new Exception());

            // Act
            var actionResult = await controller.Get(CipBuilder.BuildCip());

            // Assert
            var requestResult = actionResult.Should().BeOfType<ObjectResult>().Subject;
            requestResult.Should().NotBeNull();
            requestResult.StatusCode.Should().Equals(StatusCodes.Status400BadRequest);

            var apiResult = requestResult.Value.Should().BeOfType<ApiResult<Object>>().Subject;

            apiResult.Should().NotBeNull();
        }

        [Fact(DisplayName = "CipControllerTests - [Get] - #02 - CENÁRIO - SUCESSO")]
        [Trait("Controller - Get Payload Response Ok", "SUCCESS")]
        public async Task Retornar_Sucesso_Get()
        {
            // Arrange
            container.Resolve<Mock<ICipService>>().Setup(x => x.ObterBoletoAsync(It.IsAny<ConsultaCipRequest>()))
                .ReturnsAsync(new JObject());

            // Act
            var actionResult = await controller.Get(CipBuilder.BuildCip());

            // Assert
            var requestResult = actionResult.Should().BeOfType<ObjectResult>().Subject;
            requestResult.Should().NotBeNull();
            requestResult.StatusCode.Should().Equals(StatusCodes.Status200OK);

            var apiResult = requestResult.Value.Should().BeOfType<ApiResult<Object>>().Subject;

            apiResult.Should().NotBeNull();
        }
    }
}
