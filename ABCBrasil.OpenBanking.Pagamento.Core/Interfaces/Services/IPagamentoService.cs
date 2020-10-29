using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.Repository;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Pagamento;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Services
{
    public interface IPagamentoService
    {
        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);

        Task<SituacaoPagamentoResponse> ObterSituacaoProtocolo(SituacaoPagamentoProtocoloRequest situacaoPagamentoProtocolo);
        Task<SituacaoPagamentoResponse> ObterSituacaoIdentificador(SituacaoPagamentoIdentificadorRequest situacaoPagamentoIdentificador);
        Task<IncluirPagamentoResponse> Incluir(Models.Pagamento modelPagamento);
        Task<PagamentoExiste> PagamentoExiste(PagamentoExisteRequest modelExisteRequest);
    }
}
