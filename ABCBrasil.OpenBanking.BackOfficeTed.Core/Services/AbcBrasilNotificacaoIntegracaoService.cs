using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.ExternalApis;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using Microsoft.Extensions.Options;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class AbcBrasilNotificacaoIntegracaoService : ServiceBase, IAbcBrasilNotificacaoIntegracaoService
    {
        readonly IAbcBrasilNotificacaoIntegracao _apiIntegracao;
        readonly IApiIssuer _issuer;
        public AbcBrasilNotificacaoIntegracaoService(
            IApiIssuer issuer,
            IAbcBrasilNotificacaoIntegracao apiIntegracao,
            IOptions<AbcBrasilApiSettings> options) : base(issuer)
        {
            _apiIntegracao = apiIntegracao;
            _apiIntegracao.SetConfig(options?.Value);
            _issuer = issuer;
        }
        public void NotificarStatus(Notificacao notificacao, string protocolo)
        {
            _apiIntegracao.Notificar(notificacao, protocolo).ConfigureAwait(false);
        }

    }
}
