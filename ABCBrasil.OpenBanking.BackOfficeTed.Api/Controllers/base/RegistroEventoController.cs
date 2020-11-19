using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Controllers
{
    [ApiController]
    public class RegistroEventoController : ApiController
    {
        readonly IRegistroEventoService RegistroEventoService;

        public RegistroEventoController(IRegistroEventoService registroEventoService, IApiIssuer issuer, INotificationHandler notificationHandler,  ITraceHandler traceHandler)
            :base(issuer, notificationHandler, traceHandler)
        {
            RegistroEventoService = registroEventoService;

            if (registroEventoService != null)
            {
                registroEventoService.SetNoticationHandle(notificationHandler);
                registroEventoService.SetTraceHandle(traceHandler);
            }
        }

        protected void RegistroEvento(string payloadrequest)
        {
            //Caso não injetar a classe, não usar.
            if (RegistroEventoService == null)
                return;
            
            string ErrorMessage = JsonConvert.SerializeObject(NotificationHandler?.Notifications?
                    .Where(o => o.Type == NotificationType.Error && !string.IsNullOrEmpty(o.Code) && !string.IsNullOrEmpty(o.Message))
                    .Select(o => new BasicEntity { Code = o.Code, Message = o.Message })) ?? string.Empty;

            RegistroEventoService.IncluirEvento(payloadrequest, !NotificationHandler.IsError(), Request, ErrorMessage);
        }
    }
}
