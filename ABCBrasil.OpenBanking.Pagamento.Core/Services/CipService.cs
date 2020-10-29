using ABCBrasil.Core.Pagamento.Dsl.LIB.Services;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.Pagamento.Core.Common;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Cache;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.Pagamento.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Cip;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.Pagamento.Core.Services
{
    public class CipService : ServiceBase, ICipService
    {
        readonly IOptionsMonitor<AbcBrasilApiSettings> _apiSettings;
        readonly ICipCache _cipCache;
        readonly IAbcBrasilApiIntegracaoService _abcBrasilApiIntegracaoService;
        readonly ICipRepository _cipRepository;

        public CipService(
            IApiIssuer issuer,
            ITraceHandler traceHandler,
            INotificationHandler notificationHandler,
            IOptionsMonitor<AbcBrasilApiSettings> apiSettings,
            IAbcBrasilApiIntegracaoService abcBrasilApiIntegracaoService,
            ICipRepository cipRepository,
            ICipCache cipCache) : base(issuer)
        {
            _apiSettings = apiSettings;
            _abcBrasilApiIntegracaoService = abcBrasilApiIntegracaoService;
            _cipCache = cipCache;
            _cipRepository = cipRepository;
            if (_abcBrasilApiIntegracaoService != null)
            {
                _abcBrasilApiIntegracaoService.SetNoticationHandle(notificationHandler);
                _abcBrasilApiIntegracaoService.SetTraceHandle(traceHandler);
            }
        }

        /// <summary>
        /// Método responsável por consultar a situação de um código de barras na CIP
        /// </summary>
        /// <param name="codigoDeBarras">Número do código de barras</param>
        /// <returns>Objeto payload response com dados da situação do boleto</returns>
        public async Task<object> ObterBoletoAsync(ConsultaCipRequest consultaCipRequest)
        {
            var result = default(object);
            try
            {
                AddTrace("Validando código de pagamento: {@InformationData}", consultaCipRequest.codigoPagamento);
                //Caso venha Linha Digitavel, tenta converter em Codigo de Barras.
                var paymentBarCode = new PaymentCodeExtractorService().Make(consultaCipRequest.codigoPagamento);

                AddTrace("Validar código CRC x Usuário ", consultaCipRequest.codigoCliente);
                // Validar se código CRC pertence ao Usuário logado
                if (!await _cipRepository.ValidarCrcUsuario(consultaCipRequest.codigoCliente))
                {
                    AddNotification(Issues.se3026, Resources.FriendlyMessages.ErrorValidaCodigoClienteUsuario, NotificationType.Error);
                    return result;
                }

                var resultIntegracao = await _abcBrasilApiIntegracaoService.ObterBoletoAsync(paymentBarCode.BarCode);
                if (resultIntegracao != null && resultIntegracao?.BoletoPagamentoCompleto != null)
                {
                    if (resultIntegracao.ConsultaCIP.ToUpper().Equals("S"))
                    {
                        AddTrace("Boleto localizado com sucesso na CIP.");
                        result = resultIntegracao.BoletoPagamentoCompleto;

                        if (_apiSettings.CurrentValue.CacheActivated)
                        {
                            AddTrace("Gravando os dados da consulta cip no redis.");
                            var registrado = await _cipCache.Create(result, consultaCipRequest.codigoPagamento, _apiSettings.CurrentValue.CacheTtl);

                            AddTrace(String.Format("Dados da consulta cip {0}gravados no redis.", registrado? "":"não foi "));
                        }
                    }
                    else
                    {
                        AddNotification(Issues.se3020, Resources.FriendlyMessages.ServiceErrorBoletoNotFound, NotificationType.Error);
                    }
                }
                else
                {
                    AddNotification(Issues.se3023, JsonConvert.SerializeObject(new { resultIntegracao?.Mensagem }), NotificationType.Error);
                }
            }
            catch (Exception ex)
            {
                AddError(Issues.se3022, string.Format(Resources.FriendlyMessages.ServiceErrorObterSituacaoBoleto, consultaCipRequest.codigoPagamento), ex);
            }
            return result;
        }
    }
}
