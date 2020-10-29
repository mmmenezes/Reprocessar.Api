using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.Pagamento.Core.Common;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CallbackCore;
using AutoMapper;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Services
{
    public class CallbackCoreService : ServiceBase, ICallbackCoreService
    {
        readonly IOptionsMonitor<AbcBrasilApiSettings> _apiSettings;
        readonly ICallbackCoreRepository _callbackRepository;
        readonly IMapper _mapper;

        public CallbackCoreService(
            IApiIssuer issuer,
            ITraceHandler traceHandler,
            INotificationHandler notificationHandler,
            IOptionsMonitor<AbcBrasilApiSettings> apiSettings,
            IAbcBrasilApiIntegracaoService abcBrasilApiIntegracaoService,
            ICallbackCoreRepository callbackRepository,
            IMapper mapper
            ) : base(issuer)
        {
            _apiSettings = apiSettings;
            _callbackRepository = callbackRepository;
            _mapper = mapper;

            //add implementação service notificação Ademir.
        }


        public async Task<bool> AtualizarSituacaoPagamento(CallbackCoreDataRequest callback)
        {
            bool result = false;
            try
            {
                var pgto = _mapper.Map<Models.SituacaoPagamento>(callback);
                var resultRepo = await _callbackRepository.AtualizarSituacaoPagamento(pgto);
                if (resultRepo != null && !string.IsNullOrEmpty(resultRepo.MensagemRetorno))
                {
                    base.AddNotification(Issues.se3046, Resources.FriendlyMessages.ServiceErrorCallbackCoreAtualizarSituacao, NotificationType.Error);
                    return result;
                }
                //Adicionar notificação da situação do pagamento via serviço tibco.


                result = true;
            }
            catch (Exception ex)
            {
                base.AddError(Issues.se3047, string.Format(Resources.FriendlyMessages.ServiceErrorCallbackCoreAtualizarSituacao, callback), ex);
            }
            return result;
        }
    }
}
