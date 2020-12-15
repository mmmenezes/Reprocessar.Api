
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.ReProcessaTed;
using ABCBrasil.SegurancaApi.DSL.Libs.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Controllers
{
    [ApiVersion("1")]
    [SwaggerGroup("ReprocessarTed")]
    [ApiController, Route("api/v{version:apiVersion}/reprocessar"), Produces("application/json")]
    public class ReprocessarController : RegistroEventoController
    {
        private readonly ITedService _tedService;
        readonly ITraceHandler _traceHandler;

        readonly IApiIssuer _issuer;
        readonly IAntiCSRFService _antiCSRFService;
        public ReprocessarController(IRegistroEventoService registroEventoService, IApiIssuer issuer, INotificationHandler notificationHandler, ITraceHandler traceHandler, ITedService tedService) : base(registroEventoService, issuer, notificationHandler, traceHandler)
        {
            _tedService = tedService;
            _traceHandler = traceHandler;

            _issuer = issuer;
        }

        //[ProducesResponseType(typeof(ApiResult<ComprovanteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpGet("TedsBase64/{dtini}/{dtfim}")]
        public async Task<IActionResult> Teds64( DateTime dtini, DateTime dtfim, int cdcliente)
        {
            AddTrace("Solicitação do endpoint [BuscaTeds]");
            var teds = default(BuscaTedsResponse);
            try
            {
                teds = _tedService.BuscaTeds(new BuscaTedRequest { DTINI = dtini, DTFIM = dtfim, CDCliente = cdcliente });
                return Response<object>(teds, HttpStatusCode.OK);
           

            }
            catch (Exception ex)
            {
                base.AddError(Issues.ce2022, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return Response("", HttpStatusCode.BadRequest);
            }
            finally
            {
                AddTrace("Finalização do endpoint BuscaTeds.");
            }

        }
        [ProducesResponseType(typeof(FileStream), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(FileStream), StatusCodes.Status200OK)]
        [HttpGet ("TedsCSV/{dtini}/{dtfim}")]
        public async Task<FileContentResult> TedsCSV(DateTime dtini, DateTime dtfim, int? cdcliente = null)
        {
            AddTrace("Solicitação do endpoint [BuscaTeds]");

            var teds = default(BuscaTedsResponse);
            var result = default(FileContentResult);
            try
            { 
                teds = _tedService.BuscaTeds(new BuscaTedRequest { DTINI = dtini, DTFIM = dtfim, CDCliente = cdcliente });
                result = new FileContentResult(teds.CSVByte, "application/octet-stream")
                {
                    FileDownloadName = "Teds.csv"
                };
                return result;
            }
            catch (Exception ex)
            {
                base.AddError(Issues.ce2022, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return null;
            }
            finally
            {
                AddTrace("Finalização do endpoint BuscaTeds.");
            }

        }
        [ProducesResponseType(typeof(ApiResult<ReProcessaTed>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpPost(Name = "ReprocessaTed")]
        public async Task<IActionResult> ReprocessaTed([FromForm] UploadViewModel file)
        {
            AddTrace("Solicitação do endpoint [ReprocessaTed].");
            var result = default(ReProcessaTed);
            try
            {
                if (file == null)
            {
                base.AddNotification(Issues.ci2011, "Arquivo não encontrado.", NotificationType.Error);
                return Response("", HttpStatusCode.BadRequest);
            }

            var arquivo = _tedService.ProcessaArquivo(file);

            result = _tedService.ProcessaArquivoTed(arquivo);
                return Response(result, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                base.AddError(Issues.ce2022, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return Response("", HttpStatusCode.BadRequest);
            }
            finally
            {
                AddTrace("Finalização do endpoint PopulaTabela.");
            }
        }
    }
}
