using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.ExternalApis;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Infra.Common;
using System.Net.Http;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Infra.ExternalApis
{
    public class AbcBrasilNotificacaoIntegracao : ApiRequestHelper, IAbcBrasilNotificacaoIntegracao
    {
        TibcoNotificacaoConfig _tibcoNotificacaoConfig;
        public AbcBrasilNotificacaoIntegracao(HttpClient httpClient) : base(httpClient) {}
        public void SetConfig(AbcBrasilApiSettings abcBrasilApiSettings)
        {
            _tibcoNotificacaoConfig = abcBrasilApiSettings?.TibcoNotificacaoConfig;
        }
        public async Task Notificar(Notificacao notificacao, string protocolo)
        {
            await base.PostAsync(_tibcoNotificacaoConfig, UrlIntegracao.NOTIFICACAO_CALLBACK, notificacao, protocolo);
        }
    }
}
