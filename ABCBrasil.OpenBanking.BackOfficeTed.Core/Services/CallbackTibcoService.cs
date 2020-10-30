using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Callback;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class CallbackTibcoService : ServiceBase, ICallbackTibcoService
    {
        readonly IAbcBrasilNotificacaoIntegracaoService _apiIntegracao;
        readonly ITraceHandler _traceHandler;

        public CallbackTibcoService(IApiIssuer issuer,
            ITraceHandler traceHandler,
            INotificationHandler notificationHandler,
            IAbcBrasilNotificacaoIntegracaoService apiIntegracao) :base(issuer)
        {
            _apiIntegracao = apiIntegracao;
            _traceHandler = traceHandler;
            if (_apiIntegracao != null)
            {
                _apiIntegracao.SetNoticationHandle(notificationHandler);
                _apiIntegracao.SetTraceHandle(traceHandler);
            }
        }
        public void NotificarCliente(CallbackTibcoRequest callbackTibco)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                Notificacao notificacao = new Notificacao();
                notificacao.Url = callbackTibco.UrlNotificacao;
                notificacao.TipoAutenticacao = "";
                notificacao.Payload = callbackTibco;
                
                _apiIntegracao.NotificarStatus(notificacao, _traceHandler.CorrelationId);
            }
            catch (Exception ex)
            {
                AddTrace(Issues.se3040, "Falha ao notificar o cliente via tibco", ex);
                throw;
            }
            finally
            {
                sw.Stop();
                AddTrace("Método notificação de cliente no Tibco finalizado, tempo total gasto: {@InformationData}", sw.Elapsed);
            }
        }
    }
}
