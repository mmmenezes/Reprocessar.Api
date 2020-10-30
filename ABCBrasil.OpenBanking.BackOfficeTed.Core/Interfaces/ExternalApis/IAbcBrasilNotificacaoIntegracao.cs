using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.ExternalApis
{
    public interface IAbcBrasilNotificacaoIntegracao
    {
        void SetConfig(AbcBrasilApiSettings abcBrasilApiSettings);
        Task Notificar(Notificacao notificacao, string protocolo);
    }
}
