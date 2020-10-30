using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Comprovante;
using System;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class ComprovanteService : ServiceBase, IComprovanteService
    {
        readonly IAbcBrasilApiIntegracaoService _integraApiRepository;
        readonly IPagamentoRepository _pagamentoRepository;
        public ComprovanteService(
            IAbcBrasilApiIntegracaoService integraApiRepository,
            ITraceHandler traceHandler,
            INotificationHandler notificationHandler,
            IPagamentoRepository pagamentoRepository,
            IApiIssuer issuer
            )
            : base(issuer)
        {
            _integraApiRepository = integraApiRepository;
            _pagamentoRepository = pagamentoRepository;
        }
        /// <summary>
        /// Metado responsavel por obter comprovante de pagamento do boleto pelo protocolo
        /// </summary>
        /// <param name="comprovanteProtocolo"></param>
        /// <returns></returns>
        public async Task<ComprovanteResponse> ObterComprovante(string comprovanteProtocolo)
        {
            AddTrace("Service ObterComprovante");
            var retorno = default(ComprovanteResponse);
            try
            {
                AddTrace("Consulta o Protocolo", comprovanteProtocolo);
                var retornoProtocolo = await _pagamentoRepository.SituacaoProtocolo(comprovanteProtocolo);
                retorno = new ComprovanteResponse();
                if (retornoProtocolo == null)
                {
                    AddNotification(Issues.se3046, Resources.FriendlyMessages.ErrorValidaCodigoClienteUsuario, NotificationType.Error);
                    retorno.Mensagem = "Protocolo não pertece ao Usuario";
                    return retorno;
                }

                var retornoCore = await _integraApiRepository.ObterComprovante(comprovanteProtocolo);
                if (retornoCore == null)
                {
                    AddNotification(Issues.se3049, Resources.FriendlyMessages.ErrorValidaCodigoPagamento, NotificationType.Error);
                    retorno.Mensagem = "Não foram encontrados comprovantes para o protocolo informado";
                    return retorno;
                }
                
                if (retornoCore.Data.Count == 0)
                {
                    AddNotification(Issues.ci2033, Resources.FriendlyMessages.ErrorValidaCodigoPagamentoEmpty, NotificationType.Information);
                    retorno.Mensagem = "Não foram encontrados comprovantes para o protocolo informado";

                    return retorno;
                }
                AddTrace("Situação do Comprovante", retorno);

                foreach (var item in retornoCore.Data)
                {
                    if (item.NomeArquivo == "ComprovantesBoleto.pdf")
                    {
                        retorno.ArquivoGerado = item.ArquivoGerado;
                        retorno.NomeArquivo = $"Comprovante de Pagamento {DateTime.Now.ToString("_ddMMyy_")} {comprovanteProtocolo}";
                        retorno.Flgerado = item.Flgerado;
                        retorno.Mensagem = item.Mensagem;
                    }
                    else
                    {
                        if (retorno.Mensagem == null)
                        {
                            AddNotification(Issues.ci2033, Resources.FriendlyMessages.ErrorValidaCodigoPagamentoMin44, NotificationType.Information);
                            retorno.Mensagem = "Não foram encontrados comprovantes para o protocolo informado";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddError(Issues.se3014, Resources.FriendlyMessages.ErrorValidaCodigoPagamentoMin44, ex);
            }
            return retorno;
        }
    }
}
