﻿
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using ABCBrasil.SegurancaApi.DSL.Libs.Interfaces;
using Csv;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
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
        [HttpGet(Name = "BuscaTeds/{dtini}/{dtfim}/{qtd}")]
        public async Task<IActionResult> PopulaTabela( DateTime dtini, DateTime dtfim, int qtd)
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
        public async Task<IActionResult> ReprocessaTed([FromForm] UploadViewModel file)
        {
            AddTrace("Solicitação do endpoint [ReprocessaTed].");
            var result = default(IList<TransferenciaModel>);
            if (file == null)
            {
                base.AddNotification(Issues.ci2011, "Arquivo não encontrado.", NotificationType.Error);
                return Response("", HttpStatusCode.BadRequest);
            }

            var retorno = _tedService.Processaarquivo(file);

            var teste = _tedService.ProcessaTed(retorno);

            try
            {
                 return Response("", HttpStatusCode.OK);
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

            //var files = fileOne;
            return Response("", HttpStatusCode.BadRequest); ;
        }
    }
}
