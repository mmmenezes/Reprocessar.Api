//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
//using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
//using AutoMapper;
//using Castle.Windsor;
//using FluentAssertions;
//using Microsoft.Extensions.Options;
//using Moq;
//using System;
//using System.Threading.Tasks;
//using Xunit;

//namespace ABCBrasil.OpenBanking.BackOfficeTed.Tests.Unit.Services
//{
//    public class ClientServiceTests
//    {
//        readonly IWindsorContainer _container;
//        IClientService _service;

//        public ClientServiceTests()
//        {
//            _container = new WindsorContainer();
//            _container.Install(new BaseInstaller<ClientService>());

//            _container.Resolve<Mock<IOptions<AbcBrasilApiSettings>>>()
//                .Setup(x => x.Value)
//                .Returns(new AbcBrasilApiSettings { });

//            _service = _container.Resolve<ClientService>();
//        }

//        [Fact(DisplayName = "ClientServiceTests - [FindAsync] - #01 - CENÁRIO - EXCEÇÃO")]
//        [Trait("Core - FindAsync Exception", "ERROR")]
//        public async Task Retornar_Excecao_FindAsync()
//        {
//            // Arrange
//            Builder.ArrangeServiceIssuer(_container, Issues.se3002);

//            _container.Resolve<Mock<IClientRepository>>().Setup(x => x.FindAsync(It.IsAny<Guid>()))
//              .Throws(new Exception());

//            // Act
//            var actionResult = await _service.FindAsync(It.IsAny<Guid>());

//            // Assert
//            actionResult.Should().BeNull();
//        }

//        [Fact(DisplayName = "ClientServiceTests - [FindAsync] - #02 - CENÁRIO - SUCESSO")]
//        [Trait("Core - FindAsync Payload Response Ok", "SUCCESS")]
//        public async Task Retornar_Sucesso_FindAsync()
//        {
//            // Arrange
//            var config = new MapperConfiguration(opts =>
//            {
//                opts.CreateMap<Client, ClientViewModel>();
//            });
//            var mapper = config.CreateMapper();

//            Mock<IClientRepository> mockRepo = new Mock<IClientRepository>();
//            var model = new Client() { Key = Guid.NewGuid() };
//            mockRepo.Setup(m => m.FindAsync(It.IsAny<Guid>()))
//            .ReturnsAsync(model);

//            Mock<IApiIssuer> mockIssuer = new Mock<IApiIssuer>();

//            _container.Resolve<Mock<IMapper>>().Setup(x => x.Map<ClientViewModel>(new Client()))
//                .Returns(new ClientViewModel() { Key = Guid.NewGuid() });

//            ClientService _service = new ClientService(mockRepo.Object, mockIssuer.Object, mapper);
//            // Act
//            var actionResult = await _service.FindAsync(It.IsAny<Guid>());

//            // Assert
//            actionResult.Should().NotBeNull();
//            var requestResult = actionResult.Should().BeOfType<ClientViewModel>().Subject;
//            requestResult.Should().NotBeNull();
//            requestResult.Key.Should().NotBeEmpty();
//        }
//    }
//}
