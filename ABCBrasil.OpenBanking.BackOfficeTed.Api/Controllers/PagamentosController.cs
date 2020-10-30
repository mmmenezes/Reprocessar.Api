using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Validators;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Pagamento;
using ABCBrasil.SegurancaApi.DSL.Libs.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Controllers
{
    [ApiVersion("1")]
    [SwaggerGroup("Pagamentos")]
    [ApiController, Route("api/v{version:apiVersion}/pagamentos"), Produces("application/json")]
    public class PagamentosController : RegistroEventoController
    {
        readonly IPagamentoService _pagamentoService;
        readonly ITraceHandler _traceHandler;
        readonly IMapper _mapper;
        readonly IApiIssuer _issuer;
        readonly IAntiCSRFService _antiCSRFService;

        public PagamentosController(
            IPagamentoService paymentService,
            IRegistroEventoService registroEventoService,
            IApiIssuer issuer,
            IMapper mapper,
            INotificationHandler notificationHandler,
            ITraceHandler traceHandler,
            IAntiCSRFService antiCSRFService)
            : base(registroEventoService, issuer, notificationHandler, traceHandler)
        {
            _antiCSRFService = antiCSRFService;
            _pagamentoService = paymentService;
            if (_pagamentoService != null)
            {
                _pagamentoService.SetNoticationHandle(notificationHandler);
                _pagamentoService.SetTraceHandle(traceHandler);
            }
            _traceHandler = traceHandler;
            _mapper = mapper;
            _issuer = issuer;
        }

        [ProducesResponseType(typeof(ApiResult<IncluirPagamentoResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpPost(Name = "EnviaPagto")]
        public async Task<IActionResult> Post([FromBody] IncluirPagamentoRequest request)
        {
            _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();

            var result = default(IncluirPagamentoResponse);
            try
            {
                AddTrace("Solicitação de pagamento: {@InformationData}.", request);

                await ValidateAsync<IncluirPagamentoRequest>(request, new IncluirPagamentoRequestValidator(Issuer));
                if (NotificationHandler.IsError())
                {
                    AddTrace("Falha na validação dos dados para inclusão do pagamento.", Issues.ci2015);
                    return Response(default(IncluirPagamentoResponse), HttpStatusCode.BadRequest);
                }

                // Checa se existe o pagamento
                var modelExisteRequest = _mapper.Map<PagamentoExisteRequest>(request);
                var pagtoExiste = await _pagamentoService.PagamentoExiste(modelExisteRequest);

                if(pagtoExiste?.IdExiste ?? false)
                    return Response(pagtoExiste.IncluirPagamentoResponse, pagtoExiste.Sucesso ? HttpStatusCode.OK : HttpStatusCode.BadRequest);

                // Incluir Pagamento
                var modelPagamento = _mapper.Map<Core.Models.Pagamento>(request);
                modelPagamento.Protocolo = Guid.Parse(_traceHandler.CorrelationId);
                modelPagamento.LoginOpenBanking = _antiCSRFService.Login;
                // todo Gameiro: alterar nome controller para a de callback
                modelPagamento.CallBackOrigem.Url = Url.ActionLink("InsertClient", "ClientsController") ;
                result = await _pagamentoService.Incluir(modelPagamento);

                return Response(result, result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                base.AddError(Issues.ce2006, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return Response(result, HttpStatusCode.BadRequest);
            }
            finally
            {
                base.AddTrace("Solicitação de pagamento finalizada");
            }
        }

        [ProducesResponseType(typeof(ApiResult<SituacaoProtocoloPagamentoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpGet("ConsultaSituacaoProtocolo/{protocolo}", Name = "ConsultaSituacaoProtocolo")]
        public async Task<IActionResult> GetSituacaoProtocolo([FromRoute] SituacaoPagamentoProtocoloRequest  protocoloPagamentoRequest)
        {
            AddTrace("Solicitação do endpoint [Situacao por Protocolo].");
            var result = default(object);

            var infoData = JsonConvert.SerializeObject(protocoloPagamentoRequest);
            try
            {
                _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();

                await base.ValidateAsync<SituacaoPagamentoProtocoloRequest>(protocoloPagamentoRequest, new SituacaoPagamentoProtocoloValidator(Issuer));
                if (NotificationHandler.IsError())
                {
                    base.AddTrace("Falha na validação dos dados para a Situação do Pagamento por Protocolo.", Issues.ci2030);
                    return base.Response(default(ApiResult), HttpStatusCode.BadRequest);
                }
                base.AddTrace("Chamando service. {@InformationData}", infoData);
                result = await _pagamentoService.ObterSituacaoProtocolo(protocoloPagamentoRequest);

                return base.Response(result, result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                base.AddError(Issues.ce2020, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return Response(result, HttpStatusCode.BadRequest);
            }
            finally
            {
                base.RegistroEvento(infoData);
                AddTrace("Finalização do endpoint.");
            }
        }

        [ProducesResponseType(typeof(ApiResult<SituacaoProtocoloPagamentoResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpGet("ConsultaSituacaoIdentificador/{identificadorPagamento}", Name = "ConsultaSituacaoIdentificador")]
        public async Task<IActionResult> GetSituacaoIdentificador([FromRoute] SituacaoPagamentoIdentificadorRequest situacaoPagamentoIdentificadorRequest)
        {
            AddTrace("Solicitação do endpoint [Situacao por Identificador].");
            var result = default(object);

            var infoData = JsonConvert.SerializeObject(situacaoPagamentoIdentificadorRequest);
            try
            {
                _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();

                await ValidateAsync<SituacaoPagamentoIdentificadorRequest>(situacaoPagamentoIdentificadorRequest, new SituacaoPagamentoIdentificadorValidator(Issuer));
                if (NotificationHandler.IsError())
                {
                    AddTrace("Falha na validação dos dados para a Situação do Pagamento por Identificador.", Issues.ci2031);
                    return base.Response(default(ApiResult), HttpStatusCode.BadRequest);
                }
                AddTrace("Chamando service. {@InformationData}", infoData);
                result = await _pagamentoService.ObterSituacaoIdentificador(situacaoPagamentoIdentificadorRequest);

                return base.Response(result, result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                AddError(Issues.ce2021, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return base.Response(result, HttpStatusCode.BadRequest);
            }
            finally
            {
                base.RegistroEvento(infoData);
                AddTrace("Finalização do endpoint.");
            }
        }

    }
}
