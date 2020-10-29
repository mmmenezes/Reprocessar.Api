using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Comprovante;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Services
{
    public interface IComprovanteService
    {
        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);
        Task<ComprovanteResponse> ObterComprovante(string comprovanteProtocolo);
    }
}
