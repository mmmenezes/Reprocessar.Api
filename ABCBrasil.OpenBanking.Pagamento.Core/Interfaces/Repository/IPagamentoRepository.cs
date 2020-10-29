using ABCBrasil.OpenBanking.Pagamento.Core.Models.Repository;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Pagamento;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Repository
{
    public interface IPagamentoRepository
    {
        Task<PagamentoExisteResponse> PagamentoExiste(PagamentoExisteRequest pagamento);
        Task<bool> ContaPertenceCliente(Models.Pagamento pagamento);
        Task<SituacaoPagamentoResponse> SituacaoProtocolo(string protocolo);
        Task<SituacaoPagamentoResponse> SituacaoIdentificador(string identificadorPagamento);
    }
}
