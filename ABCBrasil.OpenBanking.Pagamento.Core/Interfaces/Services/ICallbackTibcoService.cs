using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Callback;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Services
{
    public interface ICallbackTibcoService
    {
        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);
        void NotificarCliente(CallbackTibcoRequest callbackTibco);
    }
}
