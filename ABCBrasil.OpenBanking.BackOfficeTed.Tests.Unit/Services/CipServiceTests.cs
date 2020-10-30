
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Cache;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.CoreCip;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Cip;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using Castle.Windsor;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Tests.Unit.Services
{
    public class CipServiceTests
    {
        readonly IWindsorContainer _container;
        ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services.ICipService _service;

        public CipServiceTests()
        {
            _container = new WindsorContainer();
            _container.Install(new BaseInstaller<CipService>());

            _container.Resolve<Mock<IOptions<AbcBrasilApiSettings>>>()
                .Setup(x => x.Value)
                .Returns(new AbcBrasilApiSettings { });

            _service = _container.Resolve<CipService>();
        }

        [Fact(DisplayName = "CipServiceTests - [ObterBoletoAsync] - #01 - CENÁRIO - EXCEÇÃO")]
        [Trait("Core - ObterBoletoAsync Exception", "ERROR")]
        public async Task Retornar_Excecao_ObterBoletoAsync()
        {
            // Arrange
            Builder.ArrangeServiceIssuer(_container, Issues.se3002);

            _container.Resolve<Mock<IClientRepository>>().Setup(x => x.FindAsync(It.IsAny<Guid>()))
              .Throws(new Exception());

            // Act
            var data = await _service.ObterBoletoAsync(new Core.ViewModels.Arguments.Cip.ConsultaCipRequest() { codigoCliente = It.IsAny<int>(), codigoPagamento = It.IsAny<string>() });

            // Assert
            Assert.Null(data);
        }

        [Fact(DisplayName = "CipServiceTests - [ObterBoletoAsync] - #02 - CENÁRIO - SUCESSO")]
        [Trait("Core - ObterBoletoAsync Payload Response Ok", "SUCCESS")]
        public async Task Retornar_Sucesso_ObterBoletoAsync()
        {
            //Arrange
            var bol = new Mock<BoletoCipResult>();
            bol.Object.BoletoPagamentoCompleto = new BoletoPagamentoCompleto();
            bol.Object.ConsultaCIP = "S";
            bol.Object.Sucesso = true;

            Mock<IApiIssuer> mockIssuer = new Mock<IApiIssuer>();
            Mock<ITraceHandler> mockITraceHandler = new Mock<ITraceHandler>();
            Mock<INotificationHandler> mockINotificationHandler = new Mock<INotificationHandler>();
            Mock<ICipCache> mockICipCache = new Mock<ICipCache>();

            Mock<ICipRepository> mockICipRepository = new Mock<ICipRepository>();
            mockICipRepository.Setup(x => x.ValidarCrcUsuario(It.IsAny<int>())).Returns(Task.FromResult(true));

            Mock<IOptionsMonitor<AbcBrasilApiSettings>> mockIOptAbcSet = new Mock<IOptionsMonitor<AbcBrasilApiSettings>>();
            mockIOptAbcSet.Setup(x => x.CurrentValue).Returns(new AbcBrasilApiSettings() { CacheActivated = false });


            var mockAbcBrasilIntegracaoService = new Mock<IAbcBrasilApiIntegracaoService>();
            mockAbcBrasilIntegracaoService.Setup(x => x.ObterBoletoAsync(It.IsAny<string>()))
                   .ReturnsAsync(bol.Object);

            _service = new CipService(mockIssuer.Object, mockITraceHandler.Object, mockINotificationHandler.Object, mockIOptAbcSet.Object, mockAbcBrasilIntegracaoService.Object, mockICipRepository.Object, mockICipCache.Object);

            //Act
            var data = await _service.ObterBoletoAsync(Builder.CipBuilder.BuildCip());

            //Assert
            Assert.NotNull(data);
            //Assert.True(data.HasValues);
            //Assert.True(data.Count == 15);
            //Assert.IsType<JObject>(data);
        }
    }
}
