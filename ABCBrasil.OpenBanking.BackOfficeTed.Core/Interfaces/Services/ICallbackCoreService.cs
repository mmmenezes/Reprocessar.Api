using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.CallbackCore;
using System.Threading.Tasks;


namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services
{
    public interface ICallbackCoreService
    {
        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);
        Task<bool> AtualizarSituacaoPagamento(CallbackCoreDataRequest callback);
    }
}
