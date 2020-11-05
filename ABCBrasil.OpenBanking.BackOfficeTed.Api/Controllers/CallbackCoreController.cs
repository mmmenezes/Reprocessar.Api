using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Api.Controllers
{
    //[ApiVersion("1")]
    //[SwaggerGroup("CallbackCore")]
    //[ApiController, Route("api/v{version:apiVersion}/callbackcore/pagamentos"), Produces("application/json")]
    //public class CallbackCoreController : RegistroEventoController
    //{
    //    readonly ICallbackCoreService _callbackCoreService;
    //    readonly ITraceHandler _traceHandler;

    //    public CallbackCoreController(
    //       IApiIssuer issuer,
    //       IRegistroEventoService registroEventoService,
    //       INotificationHandler notificationHandler,
    //       ITraceHandler traceHandler,
    //       ICallbackCoreService callbackCoreService)
    //       : base(registroEventoService, issuer, notificationHandler, traceHandler)
    //    {
    //        _callbackCoreService = callbackCoreService;
    //        if (_callbackCoreService != null)
    //        {
    //            _callbackCoreService.SetNoticationHandle(notificationHandler);
    //            _callbackCoreService.SetTraceHandle(traceHandler);
    //        }
    //        _traceHandler = traceHandler;
    //    }

    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    [ProducesResponseType(typeof(ApiResult<string>), StatusCodes.Status400BadRequest)]
    //    [HttpPost(Name = "Pagamento")]
    //    public async Task<IActionResult> Post([FromBody] CallbackCoreDataRequest command)
    //    {
    //        _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();
    //        try
    //        {
    //            AddTrace("Inicio callback core de pagamentos.", command);
    //            await base.ValidateAsync<CallbackCoreDataRequest>(command, new CallbackCoreValidator(Issuer));
    //            if (NotificationHandler.IsError())
    //            {
    //                AddTrace("Falha na validação dos dados do callback core de pagamentos.", Issues.ci2035);
    //                return Response(default(string), HttpStatusCode.BadRequest);
    //            }
    //            AddTrace("Atualização de pagamento via callback core de pagamento", command);
    //            var resultAtualizarSituacaoService = await _callbackCoreService.AtualizarSituacaoPagamento(command);
    //            AddTrace("Resultado da atualização de pagamento via callback core de pagamento", resultAtualizarSituacaoService);
                
    //            return Response(default(string), (resultAtualizarSituacaoService)  ? HttpStatusCode.NoContent : HttpStatusCode.BadRequest);
    //        }
    //        catch (Exception ex)
    //        {
    //            AddError(Issues.ce2001, Core.Resources.FriendlyMessages.GeneralFail, ex);
    //            return Response(default(string), HttpStatusCode.BadRequest);
    //        }
    //        finally
    //        {
    //            AddTrace("Finalização do método");
    //        }
    //    }
    //}
}
