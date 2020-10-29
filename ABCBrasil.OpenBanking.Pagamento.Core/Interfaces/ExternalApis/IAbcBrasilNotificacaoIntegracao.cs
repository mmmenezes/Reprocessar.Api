using ABCBrasil.OpenBanking.Pagamento.Core.Common;
using ABCBrasil.OpenBanking.Pagamento.Core.Models;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.ExternalApis
{
    public interface IAbcBrasilNotificacaoIntegracao
    {
        void SetConfig(AbcBrasilApiSettings abcBrasilApiSettings);
        Task Notificar(Notificacao notificacao, string protocolo);
    }
}
