using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Api.Controllers
{
    //[ApiVersion("1")]
    //[SwaggerGroup("Comprovantes")]
    //[ApiController, Route("api/v{version:apiVersion}/comprovantes"), Produces("application/json")]
    //public class ComprovantesController : RegistroEventoController
    //{
    //    readonly IComprovanteService _comprovateService;
    //    readonly ITraceHandler _traceHandler;
    //    public ComprovantesController(
    //   IComprovanteService comprovanteService,
    //   IRegistroEventoService registroEventoService,
    //   IApiIssuer issuer,
    //   INotificationHandler notificationHandler,
    //   ITraceHandler traceHandler
    //   )
    //   : base(registroEventoService, issuer, notificationHandler, traceHandler)
    //    {
    //        _comprovateService = comprovanteService;
    //        if (_comprovateService != null)
    //        {
    //            _comprovateService.SetNoticationHandle(notificationHandler);
    //            _comprovateService.SetTraceHandle(traceHandler);
    //        }
    //        _traceHandler = traceHandler;
    //    }

    //    [ProducesResponseType(typeof(ApiResult<ComprovanteResponse>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    //    [HttpGet("{protocolo}", Name = "ConsultaComprovante")]
    //    public async Task<IActionResult> GetComprovante([FromRoute] string protocolo)
    //    {
    //        _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();

    //        AddTrace("Solicitação do endpoint [Comprovante por Protocolo].");
    //        var result = default(ComprovanteResponse);
    //        try
    //        {
    //            await base.ValidateAsync<string>(protocolo, new ComprovanteValidator(Issuer));
    //            if (NotificationHandler.IsError())
    //            {
    //                base.AddTrace("Falha na validação dos dados para a comprovante de Pagamento por Protocolo.", Issues.ci2034);
    //                return base.Response(default(ApiResult), HttpStatusCode.BadRequest);
    //            }
    //            base.AddTrace("Chamando service. {@InformationData}", protocolo);

    //            result = await _comprovateService.ObterComprovante(protocolo);

    //            return base.Response(result, result.Flgerado ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
    //        }
    //        catch (Exception ex)
    //        {
    //            base.AddError(Issues.ce2022, Core.Resources.FriendlyMessages.GeneralFail, ex);
    //            return Response(result, HttpStatusCode.BadRequest);
    //        }
    //        finally
    //        {
    //            base.RegistroEvento(protocolo);
    //            AddTrace("Finalização do endpoint.");
    //        }
    //    }


    //}
}
