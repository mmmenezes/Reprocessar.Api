using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CallbackCore;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Repository
{
    public interface ICallbackCoreRepository
    {
        Task<CallbackCoreResponse> AtualizarSituacaoPagamento(Models.SituacaoPagamento situacaoPagamento);
    }
}
