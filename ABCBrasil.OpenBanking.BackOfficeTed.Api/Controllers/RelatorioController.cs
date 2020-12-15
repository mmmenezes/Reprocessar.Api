
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.ReProcessaTed;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Controllers
{
    [ApiVersion("1")]
    [SwaggerGroup("Relatorios")]
    [ApiController, Route("api/v{version:apiVersion}/Relatorio"), Produces("application/json")]
    public class RelatorioController : RegistroEventoController
    {
        private readonly IRelatorioService _relatorioService;
        readonly ITraceHandler _traceHandler;
        private IWebHostEnvironment _webHostEnvironmen;
        readonly IApiIssuer _issuer;

        public RelatorioController(IRegistroEventoService registroEventoService, IApiIssuer issuer, INotificationHandler notificationHandler, ITraceHandler traceHandler, IRelatorioService relatorioService, IWebHostEnvironment webHostEnvironmen) : base(registroEventoService, issuer, notificationHandler, traceHandler)
        {
            _relatorioService = relatorioService;
            _traceHandler = traceHandler;
            _issuer = issuer;
            _webHostEnvironmen = webHostEnvironmen;
        }

        [ProducesResponseType(typeof(ApiResult<ReProcessaTed>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpGet("Relatorio/{dtini}/{dtfim}")]
        public async Task<IActionResult> GerarRelatorioApis(DateTime dtini, DateTime dtfim, int cdcliente)
        {
            AddTrace("Solicitação do endpoint [Relatorio]");
            string sFileName = @"Employees.xlsx";
            try
            {
                var processarDados = _relatorioService.ProcessarDados(dtini, dtfim, cdcliente);
                var arquivo = _relatorioService.GerarArquivo(Request.Scheme, Request.Host, sFileName);

                string sWebRootFolder = _webHostEnvironmen.WebRootPath;

                return Response("", HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                base.AddError(Issues.ce2022, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return Response("", HttpStatusCode.BadRequest);
            }
            finally
            {
                AddTrace("Finalização do endpoint Relatorio.");
            }

        }

    }
}
