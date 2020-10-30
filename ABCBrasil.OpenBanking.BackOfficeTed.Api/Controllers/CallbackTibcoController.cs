using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Validators.Callback;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Callback;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Controllers
{
    [ApiVersion("1")]
    [SwaggerGroup("CallbackTibco")]
    [ApiController, Route("api/v{version:apiVersion}/callbacktibco/pagamentos"), Produces("application/json")]
    public class CallbackTibcoController : ApiController
    {
        readonly ITraceHandler _traceHandler;
        readonly IApiIssuer _issuer;
        readonly ICallbackTibcoService _callbackTibcoService;
        public CallbackTibcoController(
            IApiIssuer issuer,
            ICallbackTibcoService callbackTibcoService,
            INotificationHandler notificationHandler,
            ITraceHandler traceHandler)
            : base(issuer, notificationHandler, traceHandler)
        {
            _traceHandler = traceHandler;
            _issuer = issuer;
            _callbackTibcoService = callbackTibcoService; 
        }


        [ProducesResponseType(typeof(ApiResult<CallBackTibcoResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpPost(Name = "pagamentos")]
        public async Task<IActionResult> Post([FromBody] CallbackTibcoRequest request)
        {
            _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();

            var result = default(CallBackTibcoResponse);
            try
            {
                AddTrace("Recebido o Callback do Tibco: {@InformationData}.", request);

                await ValidateAsync<CallbackTibcoRequest>(request, new CallbackTibcoValidator(Issuer));
                if (NotificationHandler.IsError())
                {
                    AddTrace("Falha na validação dos dados.", Issues.ci2015);
                    return Response(default(CallBackTibcoResponse), HttpStatusCode.BadRequest);
                }
                _callbackTibcoService.NotificarCliente(request);

                return Response(result, result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                base.AddError(Issues.ce2006, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return Response(result, HttpStatusCode.BadRequest);
            }
            finally
            {
                base.AddTrace("Calback do Tibco - termino do processamento.");
            }
        }
    }
}
