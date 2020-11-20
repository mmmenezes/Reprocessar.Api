
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using ABCBrasil.SegurancaApi.DSL.Libs.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public ReprocessarController(IRegistroEventoService registroEventoService, IApiIssuer issuer, INotificationHandler notificationHandler, ITraceHandler traceHandler,ITedService tedService) : base(registroEventoService, issuer, notificationHandler, traceHandler)
        {
            _tedService = tedService;
            _traceHandler = traceHandler;
           
            _issuer = issuer;
        }

        //[ProducesResponseType(typeof(ApiResult<ComprovanteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpGet( Name = "BuscaTeds/{dtini}/{dtfim}/{qtd}")]
        public async Task<IActionResult> PopulaTabela(DateTime dtini,DateTime dtfim,int qtd)
        {
            AddTrace("Solicitação do endpoint [BuscaTeds]");
            try
            {
                var teds = _tedService.BuscaTeds(new BuscaTedRequest { DTINI = dtini, DTFIM = dtfim, QTD = qtd });
                return Response<object>(teds, HttpStatusCode.OK);
               
            }
            catch (Exception ex)
            {
                base.AddError(Issues.ce2022, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return Response("", HttpStatusCode.BadRequest);
            }
            finally
            {
                //await base.IncluirLog(teds);
                AddTrace("Finalização do endpoint BuscaTeds.");
            }

        }
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpPost(Name = "ReprocessaTed")]
        public async Task<IActionResult> ReprocessaTed(string SelectedCSV)
        {
            AddTrace("Solicitação do endpoint [PopulaTabela].");
            var res = _tedService.ProcessaTed(SelectedCSV);
            
            try
            {
                return Response<bool>(res, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                base.AddError(Issues.ce2022, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return Response("", HttpStatusCode.BadRequest);
            }
            finally
            {
                //await base.IncluirLog(teds);
                AddTrace("Finalização do endpoint PopulaTabela.");
            }

            
            
        }
    }
}
