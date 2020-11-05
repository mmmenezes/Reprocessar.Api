using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.SegurancaApi.DSL.Libs.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Api.Controllers
{
    //[ApiVersion("1")]
    //[SwaggerGroup("Pagamentos")]
    //[ApiController, Route("api/v{version:apiVersion}/pagamentos"), Produces("application/json")]
    //public class PagamentosController : RegistroEventoController
    //{
    //    readonly IPagamentoService _pagamentoService;
    //    readonly ITraceHandler _traceHandler;
    //    readonly IMapper _mapper;
    //    readonly IApiIssuer _issuer;
    //    readonly IAntiCSRFService _antiCSRFService;
    //    readonly string _apiKeyName = Startup.Config.GetSection("ABCBrasilApiSettings:keyName").Value;
    //    readonly string _apiKeyValue = Startup.Config.GetSection("ABCBrasilApiSettings:keyValue").Value;
    //    readonly decimal _valorAcima = Convert.ToDecimal(Startup.Config.GetSection("PagamentoConfig:LimiteValorAcima").Value);

    //    public PagamentosController(
    //        IPagamentoService paymentService,
    //        IRegistroEventoService registroEventoService,
    //        IApiIssuer issuer,
    //        IMapper mapper,
    //        INotificationHandler notificationHandler,
    //        ITraceHandler traceHandler,
    //        IAntiCSRFService antiCSRFService)
    //        : base(registroEventoService, issuer, notificationHandler, traceHandler)
    //    {
    //        _antiCSRFService = antiCSRFService;
    //        _pagamentoService = paymentService;
    //        if (_pagamentoService != null)
    //        {
    //            _pagamentoService.SetNoticationHandle(notificationHandler);
    //            _pagamentoService.SetTraceHandle(traceHandler);
    //        }
    //        _traceHandler = traceHandler;
    //        _mapper = mapper;
    //        _issuer = issuer;
    //    }

    //    [ProducesResponseType(typeof(ApiResult<IncluirPagamentoResponse>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    //    [HttpPost(Name = "EnviaPagto")]
    //    public async Task<IActionResult> Post([FromBody] IncluirPagamentoRequest request)
    //    {
    //        _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();

    //        var result = default(IncluirPagamentoResponse);
    //        try
    //        {
    //            AddTrace("Solicitação de pagamento: {@InformationData}.", request);

    //            await ValidateAsync<IncluirPagamentoRequest>(request, new IncluirPagamentoRequestValidator(Issuer));
    //            if (NotificationHandler.IsError())
    //            {
    //                AddTrace("Falha na validação dos dados para inclusão do pagamento.", Issues.ci2015);
    //                return Response(default(IncluirPagamentoResponse), HttpStatusCode.BadRequest);
    //            }

    //            #region Idempotencia de Pagamento
    //            // Checa se existe o pagamento
    //            var modelExisteRequest = _mapper.Map<PagamentoExisteRequest>(request);
    //            var pagtoExiste = await _pagamentoService.PagamentoExiste(modelExisteRequest);

    //            if(pagtoExiste?.IdExiste ?? false)
    //                return Response(pagtoExiste.IncluirPagamentoResponse, pagtoExiste.Sucesso ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
    //            #endregion

    //            // Incluir Pagamento
    //            request.ValorAcima = _valorAcima;
    //            var modelPagamento = _mapper.Map<Core.Models.Pagamento>(request);
    //            modelPagamento.Protocolo = Guid.Parse(_traceHandler.CorrelationId);
    //            modelPagamento.LoginOpenBanking = _antiCSRFService.Login;
    //            modelPagamento.CallBackTibcoOrigem.Url = Url.ActionLink("pagamentos", "callbacktibco");
    //            modelPagamento.CallBackTibcoOrigem.ApiKeyName = _apiKeyName;
    //            modelPagamento.CallBackTibcoOrigem.ApiKeyValue = _apiKeyValue;
    //            modelPagamento.CallBackOrigem.Url = Url.ActionLink("pagamentos", "callbackcore");
    //            modelPagamento.CallBackOrigem.ApiKeyName = _apiKeyName;
    //            modelPagamento.CallBackOrigem.ApiKeyValue = _apiKeyValue;

    //            result = await _pagamentoService.Incluir(modelPagamento);

    //            return Response(result, result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
    //        }
    //        catch (Exception ex)
    //        {
    //            base.AddError(Issues.ce2006, Core.Resources.FriendlyMessages.GeneralFail, ex);
    //            return Response(result, HttpStatusCode.BadRequest);
    //        }
    //        finally
    //        {
    //            base.AddTrace("Solicitação de pagamento finalizada");
    //        }
    //    }

    //    [ProducesResponseType(typeof(ApiResult<SituacaoProtocoloPagamentoResponse>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    //    [HttpGet("ConsultaSituacaoProtocolo/{protocolo}", Name = "ConsultaSituacaoProtocolo")]
    //    public async Task<IActionResult> GetSituacaoProtocolo([FromRoute] SituacaoPagamentoProtocoloRequest  protocoloPagamentoRequest)
    //    {
    //        _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();
    //        AddTrace("Solicitação do endpoint [Situacao por Protocolo].");
    //        var result = default(object);

    //        var infoData = JsonConvert.SerializeObject(protocoloPagamentoRequest);
    //        try
    //        {

    //            await base.ValidateAsync<SituacaoPagamentoProtocoloRequest>(protocoloPagamentoRequest, new SituacaoPagamentoProtocoloValidator(Issuer));
    //            if (NotificationHandler.IsError())
    //            {
    //                base.AddTrace("Falha na validação dos dados para a Situação do Pagamento por Protocolo.", Issues.ci2030);
    //                return base.Response(default(ApiResult), HttpStatusCode.BadRequest);
    //            }
    //            base.AddTrace("Chamando service. {@InformationData}", infoData);
    //            result = await _pagamentoService.ObterSituacaoProtocolo(protocoloPagamentoRequest);

    //            return base.Response(result, result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
    //        }
    //        catch (Exception ex)
    //        {
    //            base.AddError(Issues.ce2020, Core.Resources.FriendlyMessages.GeneralFail, ex);
    //            return Response(result, HttpStatusCode.BadRequest);
    //        }
    //        finally
    //        {
    //            base.RegistroEvento(infoData);
    //            AddTrace("Finalização do endpoint.");
    //        }
    //    }

    //    [ProducesResponseType(typeof(ApiResult<SituacaoProtocoloPagamentoResponse>), StatusCodes.Status200OK)]
    //    [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
    //    [HttpGet("ConsultaSituacaoIdentificador/{identificadorPagamento}", Name = "ConsultaSituacaoIdentificador")]
    //    public async Task<IActionResult> GetSituacaoIdentificador([FromRoute] SituacaoPagamentoIdentificadorRequest situacaoPagamentoIdentificadorRequest)
    //    {
    //        _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();

    //        AddTrace("Solicitação do endpoint [Situacao por Identificador].");
    //        var result = default(object);

    //        var infoData = JsonConvert.SerializeObject(situacaoPagamentoIdentificadorRequest);
    //        try
    //        {
    //            await ValidateAsync<SituacaoPagamentoIdentificadorRequest>(situacaoPagamentoIdentificadorRequest, new SituacaoPagamentoIdentificadorValidator(Issuer));
    //            if (NotificationHandler.IsError())
    //            {
    //                AddTrace("Falha na validação dos dados para a Situação do Pagamento por Identificador.", Issues.ci2031);
    //                return base.Response(default(ApiResult), HttpStatusCode.BadRequest);
    //            }
    //            AddTrace("Chamando service. {@InformationData}", infoData);
    //            result = await _pagamentoService.ObterSituacaoIdentificador(situacaoPagamentoIdentificadorRequest);

    //            return base.Response(result, result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
    //        }
    //        catch (Exception ex)
    //        {
    //            AddError(Issues.ce2021, Core.Resources.FriendlyMessages.GeneralFail, ex);
    //            return base.Response(result, HttpStatusCode.BadRequest);
    //        }
    //        finally
    //        {
    //            base.RegistroEvento(infoData);
    //            AddTrace("Finalização do endpoint.");
    //        }
    //    }

    //}
}
