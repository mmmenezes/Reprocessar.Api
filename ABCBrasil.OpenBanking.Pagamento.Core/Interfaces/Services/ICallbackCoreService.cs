using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CallbackCore;
using System.Threading.Tasks;


namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Services
{
    public interface ICallbackCoreService
    {
        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);
        Task<bool> AtualizarSituacaoPagamento(CallbackCoreDataRequest callback);
    }
}
