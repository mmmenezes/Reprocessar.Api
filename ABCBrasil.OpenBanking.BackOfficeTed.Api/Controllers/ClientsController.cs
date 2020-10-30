using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Controllers
{
    [ApiVersion("1")]
    [SwaggerGroup("Api de Clientes")]
    [ApiController, Route("api/v{version:apiVersion}/clientes"), Produces("application/json")]
    public class ClientsController : RegistroEventoController
    {
        readonly IClientService _clientService;
        readonly ITraceHandler _traceHandler;

        public ClientsController(
            IClientService clientService,
            IApiIssuer issuer,
            IRegistroEventoService registroEventoService,
            INotificationHandler notificationHandler,
            ITraceHandler traceHandler)
            : base(registroEventoService, issuer, notificationHandler, traceHandler)
        {
            _clientService = clientService;
            if (_clientService != null)
            {
                _clientService.SetNoticationHandle(notificationHandler);
                _clientService.SetTraceHandle(traceHandler);
            }

            _traceHandler = traceHandler;
        }

        [ProducesResponseType(typeof(ApiResult<ClientViewModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpPost(Name = "InsertClient")]
        public async Task<IActionResult> Post([FromBody] RegisterClientCommand command)
        {
            _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();

            var result = default(ClientViewModel);
            try
            {
                AddTrace("Solicitação de criação de cliente.", command);

                //await ValidateAsync(command, new ClientRegisterValidator(Issuer));
                if (NotificationHandler.IsError())
                {
                    AddTrace("Falha na validação dos dados para criação de cliente.", Issues.ci2012);

                    return Response(default(ClientViewModel), HttpStatusCode.BadRequest);
                }

                result = await _clientService.CreateAsync(command);

                AddTrace("Resultado da solicitação de criação", result);

                return Response(result, result != null ? HttpStatusCode.Created : HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                AddError(Issues.ce2001, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return Response(result, HttpStatusCode.BadRequest);
            }
            finally
            {
                AddTrace("Finalização do método");
            }
        }

        [ProducesResponseType(typeof(ApiResult<ClientViewModel>), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpPut("{id}", Name = "UpdateClient")]
        public async Task<IActionResult> Put(Guid id, [FromBody] UpdateClientCommand command)
        {
            _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();

            var result = default(ClientViewModel);
            try
            {
                AddTrace("Solicitação de atualização de cliente.", command);

                if (id.Equals(Guid.Empty))
                {
                    AddNotification(Issues.ci2013, Core.Resources.FriendlyMessages.ControlerErrorClientIDInvalid, NotificationType.Error);
                    return Response(result, HttpStatusCode.BadRequest);
                }

                //await ValidateAsync(command, new ClientUpdateValidator(Issuer));
                if (NotificationHandler.IsError())
                {
                    //AddTrace(Issues.ci2014, "Falha na validação dos dados para atualização de cliente.");

                    return Response(result, HttpStatusCode.BadRequest);
                }

                result = await _clientService.UpdateAsync(id, command);

                AddTrace("Resultado da solicitação de atualização", result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
                return Response(result, result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                AddError(Issues.ce2005, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return Response(result, HttpStatusCode.BadRequest);
            }
            finally
            {
                AddTrace("Finalização do método");
            }
        }

        [ProducesResponseType(typeof(ApiResult<ClientViewModel>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpGet("{id}", Name = "GetClient")]
        public async Task<IActionResult> Get([FromRoute] Guid id)
        {
            _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();

            var result = default(ClientViewModel);
            try
            {
                AddTrace("Solicitação dos dados do cliente.", id);

                result = await _clientService.FindAsync(id);

                AddTrace("Resultado da solicitação dos dados do cliente", result);

                return Response(result, result != null ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                AddError(Issues.ce2002, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return Response(result, HttpStatusCode.BadRequest);
            }
            finally
            {
                AddTrace("Finalização do método");
            }
        }

        [ProducesResponseType(typeof(ApiResult<IEnumerable<ClientViewModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status404NotFound)]
        [HttpGet(Name = "GetAllClient")]
        public async Task<IActionResult> GetAll([FromQuery] short pageNumber = 1, [FromQuery] short rowsPerPage = 10)
        {
            _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();

            var result = default(IEnumerable<ClientViewModel>);
            try
            {
                AddTrace("Solicitação de busca de todos clientes.");

                result = await _clientService.SearchAsync(pageNumber, rowsPerPage);

                AddTrace($"Quantidade Clientes localizado: {result?.Count() ?? 0}");
                return Response(result, (result?.Any() ?? false) ? HttpStatusCode.OK : HttpStatusCode.NotFound);
            }
            catch (Exception ex)
            {
                AddError(Issues.ce2003, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return Response(result, HttpStatusCode.BadRequest);
            }
            finally
            {
                AddTrace("Finalização do método");
            }
        }

        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status404NotFound)]
        [HttpDelete("{id}", Name = "DeleteClient")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            _traceHandler.CorrelationId = Request.GetCorrelationIdFromHeader();

            var result = false;
            try
            {
                AddTrace("Solicitação de exclusão de cliente.", id);

                result = await _clientService.DeleteAsync(id);

                AddTrace("Resultado da solicitação de exclusão", result);

                return Response(result, result ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                AddError(Issues.ce2004, Core.Resources.FriendlyMessages.GeneralFail, ex);
                return Response(result, HttpStatusCode.BadRequest);
            }
            finally
            {
                AddTrace("Finalização do método");
            }
        }

    }
}
