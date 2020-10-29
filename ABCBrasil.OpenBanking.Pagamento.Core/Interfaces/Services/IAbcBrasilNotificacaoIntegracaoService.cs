using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.Pagamento.Core.Models;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Services
{
    public interface IAbcBrasilNotificacaoIntegracaoService
    {
        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);
        void NotificarStatus(Notificacao notificacao, string protocolo);
    }
}
