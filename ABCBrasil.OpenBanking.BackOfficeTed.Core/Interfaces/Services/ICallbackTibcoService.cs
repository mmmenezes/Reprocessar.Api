using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Callback;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services
{
    public interface ICallbackTibcoService
    {
        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);
        void NotificarCliente(CallbackTibcoRequest callbackTibco);
    }
}
