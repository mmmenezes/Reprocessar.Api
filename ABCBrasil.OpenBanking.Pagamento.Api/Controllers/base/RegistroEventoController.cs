using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

namespace ABCBrasil.OpenBanking.Pagamento.Api.Controllers
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
