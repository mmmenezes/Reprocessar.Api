using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Pagamento;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository
{
    public interface IPagamentoRepository
    {
        Task<PagamentoExisteResponse> PagamentoExiste(PagamentoExisteRequest pagamento);
        Task<bool> ContaPertenceCliente(Models.Pagamento pagamento);
        Task<SituacaoPagamentoResponse> SituacaoProtocolo(string protocolo);
        Task<SituacaoPagamentoResponse> SituacaoIdentificador(string identificadorPagamento);
    }
}
