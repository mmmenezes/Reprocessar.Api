using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Pagamento;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services
{
    public interface IPagamentoService
    {
        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);

        Task<SituacaoPagamentoResponse> ObterSituacaoProtocolo(SituacaoPagamentoProtocoloRequest situacaoPagamentoProtocolo);
        Task<SituacaoPagamentoResponse> ObterSituacaoIdentificador(SituacaoPagamentoIdentificadorRequest situacaoPagamentoIdentificador);

    }
}
