using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.CallbackCore;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository
{
    public interface ICallbackCoreRepository
    {
        Task<CallbackCoreResponse> AtualizarSituacaoPagamento(Models.SituacaoPagamento situacaoPagamento);
    }
}
