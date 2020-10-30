using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Validators;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Cip;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Controllers
{
    [ApiVersion("1")]
    [SwaggerGroup("Boletos")]
    [ApiController, Route("api/v{version:apiVersion}/pagamentos/cip"), Produces("application/json")]
    public class CipController : RegistroEventoController
    {
        readonly ITraceHandler _traceHandler;
        readonly ICipService _cipService;

        public CipController(
            ITraceHandler traceHandler,
            ICipService coreCipService,
            IApiIssuer issuer,
            IRegistroEventoService registroEventoService,
            INotificationHandler notificationHandler) 
            : base(registroEventoService, issuer, notificationHandler, traceHandler)
        {
            _traceHandler = traceHandler;
            _cipService = coreCipService;

            if (_cipService != null)
            {
                _cipService.SetNoticationHandle(notificationHandler);
                _cipService.SetTraceHandle(traceHandler);
            }
        }

        [ProducesResponseType(typeof(ApiResult<JObject>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpGet("{codigoCliente}/{codigoPagamento}", Name = "ObterBoleto")]
        public async Task<IActionResult> Get([FromRoute] ConsultaCipRequest consultaCipRequest)
        {
            base.AddTrace("Solicitação do endpoint [ObterBoleto].");
            var result = default(object);

            var infoData = JsonConvert.SerializeObject(consultaCipRequest);
            try
            {
                _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();

                await base.ValidateAsync<ConsultaCipRequest>(consultaCipRequest, new ConsultaCipValidator(Issuer));
                if (NotificationHandler.IsError())
                {
                    base.AddTrace("Falha na validação dos dados para a consulta cip.", Issues.ci2016);
                    return base.Response( default(JObject), HttpStatusCode.BadRequest);
                }
                base.AddTrace("Chamando service. {@InformationData}", infoData);
                result = await _cipService.ObterBoletoAsync(consultaCipRequest);
                
                return base.Response(result, result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                base.AddError(Issues.ce2010, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return base.Response(result, HttpStatusCode.BadRequest);
            }
            finally
            {
                base.RegistroEvento(infoData);
                base.AddTrace("Finalização do endpoint.");
            }
        }
    }
}
