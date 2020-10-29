using ABCBrasil.OpenBanking.Pagamento.Core.Common;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.ExternalApis;
using ABCBrasil.OpenBanking.Pagamento.Core.Models;
using ABCBrasil.OpenBanking.Pagamento.Infra.Common;
using System.Net.Http;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Infra.ExternalApis
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
