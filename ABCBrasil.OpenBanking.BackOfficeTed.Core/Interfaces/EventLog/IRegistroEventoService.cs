using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog
{
    public interface IRegistroEventoService
    {
        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);
        Task IncluirEvento(string payload, bool status, HttpRequest httprequest, string errormessage);
        Task IncluirEvento(string correlationid, string payload, bool status, string method, string endpoint, string errormessage);
    }
}
