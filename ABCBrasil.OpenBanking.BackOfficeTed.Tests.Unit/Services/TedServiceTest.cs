using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xunit;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Tests.Unit.Services
{
    public class TedServiceTest : BaseServiceTests<TedService>
    {
        ITedService _service;

        public TedServiceTest()
        {
            _service = _serviceBase;
        }

        [Fact(DisplayName = "TedServiceTest - [ProcessaArquivoTed] - #01 - CENÁRIO - EXCEÇÃO")]
        [Trait("ProcessaArquivoTed Exception", "ERROR")]
        public async Task Retornar_Excecao_ProcessaArquivoTed()
        {
            // Arrange
            Builder.ArrangeServiceIssuer(_container, Issues.se3045);
            Mock<IEventoRepository> mockEvento = new Mock<IEventoRepository>();
            Mock<IIBRepository> mockRepo = new Mock<IIBRepository>();
            Mock<ITedService> mockApi = new Mock<ITedService>();
            Mock<IApiIssuer> mockIssuer = new Mock<IApiIssuer>();
            Mock<IMapper> mockIMapper = new Mock<IMapper>();
           
          

            mockRepo.Setup(m => m.ProcessaTed(It.IsAny<TransferenciaInclui>())).Throws(new Exception());
            _container.Resolve<Mock<ITedService>>().Setup(x => x.ProcessaArquivo(It.IsAny<UploadViewModel>()))
              .Throws(new Exception());
            // Act
            _service = new TedService(mockEvento.Object, mockRepo.Object, mockIMapper.Object, mockIssuer.Object);
            var actionResult = _service.ProcessaArquivo(It.IsAny<UploadViewModel>());

            // Assert
            actionResult.Should().NotBeNull();
        }

        [Fact(DisplayName = "TedServiceTest - [ProcessaArquivoTed] - #02 - CENÁRIO - Sucess")]
        [Trait("ProcessaArquivoTed Exception", "Sucess")]
        public async Task Retornar_Sucess_ProcessaArquivoTed()
        {
            // Arrange
            Builder.ArrangeServiceIssuer(_container, Issues.se3045);
            Mock<IEventoRepository> mockEvento = new Mock<IEventoRepository>();
            Mock<IIBRepository> mockRepo = new Mock<IIBRepository>();
            Mock<ITedService> mockApi = new Mock<ITedService>();
            Mock<IApiIssuer> mockIssuer = new Mock<IApiIssuer>();
            Mock<IMapper> mockIMapper = new Mock<IMapper>();
            var fileMock = new Mock<IFormFile>();
            var content = @"Codigo Evento;Protocolo;CodigoCliente;Payload
3228479; 24148272 - f7ea - 470f - b304 - f25ce4f3eb30; 45;
            {""CodTedCliente"":""Gam048"",""CodCliente"":45,""AgenciaCliente"":""00019"",""ContaCliente"":""0016678011"",""TipoContaFavorecido"":""CC"",""BancoFavorecido"":""033"",""AgenciaFavorecido"":""0001"",""ContaFavorecido"":""123001"",""NumDocumentoFavorecido"":""31576454800"",""NomeFavorecido"":""José Gonçalves Cação"",""NumDocumentoFavorecido2"":"""",""NomeFavorecido2"":"""",""DataTransacao"":""2020-12-01T00:00:00Z"",""Finalidade"":""00110"",""Valor"":12.50,""Callback"":{ ""Url"":""http://api.webhookinbox.com/i/nMsgyYmx/in/""} }
            ";
            var fileName = "Teds.csv";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);
            writer.Write(content);
            writer.Flush();
            _container.Resolve<Mock<ITedService>>()
              .Setup(x => x.BuscaTeds(It.IsAny<BuscaTedRequest>()));
            UploadViewModel sut;
            var file = fileMock.Object;
            sut = new UploadViewModel { Teds = file };


            mockRepo.Setup(m => m.ProcessaTed(It.IsAny<TransferenciaInclui>())).Throws(new Exception()); ;
            _container.Resolve<Mock<ITedService>>().Setup(x => x.ProcessaArquivo(It.IsAny<UploadViewModel>())).Throws(new Exception()); ;
            // Act
            _service = new TedService(mockEvento.Object, mockRepo.Object, mockIMapper.Object, mockIssuer.Object);
            var actionResult = _service.ProcessaArquivo(sut);

            // Assert
           
            actionResult.Should().NotBeNull();
            actionResult.Count.Should().Equals("0");
        }


        [Fact(DisplayName = "TedServiceTest - [BuscaTeds] - #03 - CENÁRIO - EXCEÇÃO")]
        [Trait("BuscaTeds Exception", "EXCEÇÃO")]
        public async Task Retornar_Excecao_BuscaTeds()
        {
            // Arrange
            Builder.ArrangeServiceIssuer(_container, Issues.se3045);
            Mock<IEventoRepository> mockEvento = new Mock<IEventoRepository>();
            Mock<IIBRepository> mockRepo = new Mock<IIBRepository>();
            Mock<ITedService> mockApi = new Mock<ITedService>();
            Mock<IApiIssuer> mockIssuer = new Mock<IApiIssuer>();
            Mock<IMapper> mockIMapper = new Mock<IMapper>();
            var fileMock = new Mock<IFormFile>();



            mockApi.Setup(m => m.BuscaTeds(It.IsAny<BuscaTedRequest>()));
            _container.Resolve<Mock<ITedService>>().Setup(x => x.ProcessaArquivo(It.IsAny<UploadViewModel>()));
            // Act
            _service = new TedService(mockEvento.Object, mockRepo.Object, mockIMapper.Object, mockIssuer.Object);
            var actionResult = _service.BuscaTeds(It.IsAny<BuscaTedRequest>());

            // Assert

            actionResult.Should().NotBeNull();
            
        }

        [Fact(DisplayName = "TedServiceTest - [BuscaTeds] - #04 - CENÁRIO - Sucess")]
        [Trait("BuscaTeds Exception", "Sucess")]
        public async Task Retornar_Sucess_BuscaTeds()
        {
            // Arrange
            Builder.ArrangeServiceIssuer(_container, Issues.se3045);
            Mock<IEventoRepository> mockEvento = new Mock<IEventoRepository>();
            Mock<IIBRepository> mockRepo = new Mock<IIBRepository>();
            Mock<ITedService> mockApi = new Mock<ITedService>();
            Mock<IApiIssuer> mockIssuer = new Mock<IApiIssuer>();
            Mock<IMapper> mockIMapper = new Mock<IMapper>();
            var fileMock = new Mock<IFormFile>();



            mockApi.Setup(m => m.BuscaTeds(It.IsAny<BuscaTedRequest>()));
            _container.Resolve<Mock<ITedService>>().Setup(x => x.ProcessaArquivo(It.IsAny<UploadViewModel>()));
            // Act
            _service = new TedService(mockEvento.Object, mockRepo.Object, mockIMapper.Object, mockIssuer.Object);
            var actionResult = _service.BuscaTeds(Builder.Reprocessar64Builder());

            // Assert

            actionResult.Should().NotBeNull();
            actionResult.Teds.Should().NotBeNull();
        }

    }
}
