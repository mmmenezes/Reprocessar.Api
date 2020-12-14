using ABCBrasil.OpenBanking.BackOfficeTed.Api.Controllers;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.Pagamento.Tests.Unit.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Tests.Unit.Controllers
{
    public class ReprocessarControllerTests : BaseControllerTests<ReprocessarController>
    {
        readonly ReprocessarController controller;

        public ReprocessarControllerTests() : base()
        {
            base.ArrangeServiceIssuer(Issues.ci2011, Issues.ce2001);
            controller = _controllerBase;
            controller.ControllerContext.HttpContext = base.SetHeaders();
        }

        [Fact(DisplayName = "ReprocessarControllerTests Teds64- #01 - CENÁRIO - BADREQUEST ")]
        [Trait("Controller Teds64- Get Payload Response Ok", "Fail")]
       
        public async Task GetBadRequest64()
        {
            // arrange
            _container.Resolve<Mock<ITedService>>()
               .Setup(x => x.BuscaTeds(It.IsAny<BuscaTedRequest>()))
               .Throws(new Exception());
            //act
            var actionResult = await controller.Teds64(Builder.Reprocessar64Builder().DTFIM, Builder.Reprocessar64Builder().DTINI, Builder.Reprocessar64Builder().CDCliente??0);
            //assert
            var requestResult = actionResult.Should().BeOfType<ObjectResult>().Subject;
            requestResult.Should().NotBeNull();
            requestResult.StatusCode.Should().Equals(StatusCodes.Status400BadRequest);
        }


        [Fact(DisplayName = "ReprocessarControllerTests Teds64- #02 - CENÁRIO - SUCCESS")]
        [Trait("Controller - Get Payload Response Ok", "SUCCESS")]
        public async Task Get64()
        {
            // arrange
            _container.Resolve<Mock<ITedService>>()
               .Setup(x => x.BuscaTeds(It.IsAny<BuscaTedRequest>()))
               .Throws(new Exception());
            //act
            var actionResult = await controller.Teds64(Builder.Reprocessar64Builder().DTFIM, Builder.Reprocessar64Builder().DTINI, Builder.Reprocessar64Builder().CDCliente ?? 0);
            //assert
            var requestResult = actionResult.Should().BeOfType<ObjectResult>().Subject;
            requestResult.StatusCode.Should().Equals(StatusCodes.Status200OK);
        }


        [Fact(DisplayName = "ReprocessarControllerTests CSV - #01 - CENÁRIO - BADREQUEST ")]
        [Trait("Controller - Get Payload Response Ok", "Fail")]

        public async Task GetBadRequestCSV()
        {

            // arrange
            _container.Resolve<Mock<ITedService>>()
               .Setup(x => x.BuscaTeds(It.IsAny<BuscaTedRequest>()))
               .Throws(new Exception());
            //act
            var actionResult = await controller.TedsCSV(Builder.Reprocessar64Builder().DTFIM, Builder.Reprocessar64Builder().DTINI, Builder.Reprocessar64Builder().CDCliente ?? 0);
            //assert

            actionResult.Should().BeNull();


        }
        [Fact(DisplayName = "CallbackCoreControllerTests CSV- #02 - CENÁRIO - SUCCESS")]
        [Trait("Controller - Get Payload Response Ok", "SUCCESS")]
        public async Task GetCSV()
        {
            // arrange
            _container.Resolve<Mock<ITedService>>()
               .Setup(x => x.BuscaTeds(It.IsAny<BuscaTedRequest>()));
            //act
            var actionResult = await controller.TedsCSV(Builder.Reprocessar64Builder().DTFIM, Builder.Reprocessar64Builder().DTINI, Builder.Reprocessar64Builder().CDCliente ?? 0);
            //assert
            
            actionResult.Should().BeNull();
            
        }

        [Fact(DisplayName = "ReprocessarControllerTests Arquivo- #01 - CENÁRIO - BADREQUEST ")]
        [Trait("Controller - Post Payload Response Ok", "Fail")]
        public async Task PostBadRequestArquivo()
        {
            // arrange
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
            
            //act
            var actionResult = await controller.ReprocessaTed(sut);

            //assert
            var requestResult = actionResult.Should().BeOfType<ObjectResult>().Subject;
            requestResult.Should().NotBeNull();
            requestResult.StatusCode.Should().Equals(StatusCodes.Status400BadRequest);
        }

        [Fact(DisplayName = "ReprocessarControllerTests Arquivo- #01 - CENÁRIO - SUCESS ")]
        [Trait("Controller - Post Payload Response Ok", "SUCESS")]
        public async Task PostArquivo()
        {
            // arrange
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

            //act
            var actionResult = await controller.ReprocessaTed(sut);

            //assert
            var requestResult = actionResult.Should().BeOfType<ObjectResult>().Subject;
            requestResult.Should().NotBeNull();
            requestResult.StatusCode.Should().Equals(StatusCodes.Status200OK);
        }


    }
}

