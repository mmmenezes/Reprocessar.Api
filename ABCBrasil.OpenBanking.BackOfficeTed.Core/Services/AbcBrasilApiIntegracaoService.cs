using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.ExternalApis;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Calculo;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Calendario;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.CoreComprovante;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.CorePagamento;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.AbcBrasilApiIntegracao.Tibco;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Cip;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Resources;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Comprovante;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.ViewModels.Arguments.Pagamento;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class AbcBrasilApiIntegracaoService : ServiceBase, IAbcBrasilApiIntegracaoService
    {
        readonly IAbcBrasilApiIntegracao _apiIntegracao;
        readonly IApiIssuer _apiIssuer;

        public AbcBrasilApiIntegracaoService(
            IApiIssuer issuer,
            IAbcBrasilApiIntegracao apiIntegracao,
            IOptions<AbcBrasilApiSettings> options) : base(issuer)
        {
            _apiIntegracao = apiIntegracao;
            _apiIntegracao.SetConfig(options?.Value);
            _apiIssuer = issuer;
        }

        //
        // todo Gameiro: Canal e transação mocados, pendente de inclusão na base do calendário
        public async Task<DiaUtilResponse> ConsultarDiaUtil(Models.Pagamento pagamento)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                var data = new DiaUtilRequest()
                {
                    Moeda = "0",
                    TipoFeriado = "SP",
                    Data = pagamento.DataPagamento,
                    Cd_Canal = "0",
                    Id_Transacao = 4
                };

                return await _apiIntegracao.ConsultarDiaUtil(data, pagamento.Protocolo.ToString());
            }
            catch (Exception ex)
            {
                AddTrace(Issues.se3030, FriendlyMessages.ErroServicoDiaUtil, ex);
                throw;
            }
            finally
            {
                sw.Stop();
                AddTrace("Método verifica dia útil na api de calendário finalizado, tempo total gasto: {@InformationData}", sw.Elapsed);
            }
        }

        //
        // todo Gameiro: Canal e transação mocados, pendente de inclusão na base do calendário
        public async Task<DataExcecaoResponse> ConsultarDataExcecao(Models.Pagamento pagamento)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                var dataExcecao = new DataExcecaoRequest()
                {
                    Dc_DiaMesAno = pagamento.DataPagamento,
                    Cd_Canal = "0",
                    Id_Transacao = 4
                };

                return await _apiIntegracao.ConsultarDataExcecao(dataExcecao, pagamento.Protocolo.ToString());
            }
            catch (Exception ex)
            {
                AddTrace(Issues.se3031, FriendlyMessages.ErroServicoDataExcecao, ex);
                throw;
            }
            finally
            {
                sw.Stop();
                AddTrace("Método de consultar data de exceção na api de calendário finalizado, tempo total gasto: {@InformationData}", sw.Elapsed);
            }
        }

        //
        // todo Gameiro: Canal e transação mocados, pendente de inclusão na base do calendário
        public async Task<HoraTransacaoResponse> ConsultarHoraTransacao(Models.Pagamento pagamento)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                return await _apiIntegracao.ConsultarHoraTransacao(pagamento);
            }
            catch (Exception ex)
            {
                AddTrace(Issues.se3032, FriendlyMessages.ErroServicoHorarioTransacao, ex);
                throw;
            }
            finally
            {
                sw.Stop();
                AddTrace("Método de consulta hora da transação na api de calendário finalizado, tempo total gasto: {@InformationData}", sw.Elapsed);
            }
        }

        /// <summary>
        /// Método responsável por consultar na api de calendário o próximo dia útil
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="correlationId">CorrelationId gerado na api de pagamento</param>
        /// <returns>Objeto Service Result com Issues + DateTime</returns>
        public async Task<IServiceResult<DateTime>> ObterProximoDiaUtil(DateTime data, string correlationId)
        {
            var result = new ServiceResult<DateTime> { Result = DateTime.Now };
            var issues = new List<IssueDetail>();
            var sw = Stopwatch.StartNew();
            try
            {
                issues.Add(new IssueDetail { Message = $"Consulta próximo dia util {data}" });
                var resultService = await _apiIntegracao.ObterProximoDiaUtil(data, correlationId);
                issues.Add(new IssueDetail { Message = "Resultado da consulta", InformationData = resultService });

                if (resultService.Success && resultService.Data != null)
                {
                    result.Result = resultService?.Data?.Date.Value ?? DateTime.Now;
                }
                else if (resultService?.Notifications?.Count > 0)
                {
                    foreach (var item in resultService?.Notifications)
                    {
                        issues.Add(new IssueDetail { Message = item.Message });
                    }
                }
            }
            catch (Exception ex)
            {
                issues.Add(new IssueDetail
                {
                    Code = _apiIssuer.MakerCode(Issues.se3009),
                    FriendlyMessage = string.Format(FriendlyMessages.ServiceErrorExternalApi, "Calendário"),
                    Id = _apiIssuer.Maker(Issues.se3009),
                    Message = ex.Message,
                    Exception = ex,
                    Level = IssueLevelEnum.Error
                });
                throw;
            }
            finally
            {
                sw.Stop();
                AddTrace("Obter próximo dia útil finalizado, tempo total gasto: {@InformationData}", sw.Elapsed);
                result.Issues.AddRange(issues);
            }
            return result;
        }

        /// <summary>
        /// Método responsável por consultar código de barras na CIP via Api Core CIP
        /// </summary>
        /// <param name="codigoDeBarras">código de barras válidado</param>
        /// <param name="correlationId">CorrelationId gerado na api de pagamento</param>
        /// <returns>Payload response com dados do boleto</returns>
        public async Task<BoletoCipResult> ObterBoletoAsync(string codigoDeBarras)
        {
            var result = default(BoletoCipResult);
            var sw = Stopwatch.StartNew();
            try
            {
                AddTrace("Efetuando request API Core CIP para consulta código de barras");
                result = await _apiIntegracao.ObterBoletoAsync(codigoDeBarras, base.GetCorrelation());

                if (result?.BoletoPagamentoCompleto != null)
                {
                    return result;
                }
                else
                {
                    AddTrace(string.Format(FriendlyMessages.ServiceErrorExternalApi, "Core Cip {@InformationData}"), result);
                }

                AddTrace("Resultado da consulta {@InformationData}", result?.Sucesso.Value);

            }
            catch (Exception ex)
            {
                AddError(Issues.se3010, string.Format(FriendlyMessages.ServiceErrorExternalApi, "Core Cip"), ex);
            }
            finally
            {
                sw.Stop();
                AddTrace("Tempo total gasto request API Core CIP {@InformationData}", sw.Elapsed);
            }
            return result;
        }

        public async Task<CalculaTituloResponse> CalcularTituloAsync(Models.Pagamento pagamento)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                var calcularRequest = new CalculaTituloRequest()
                {
                    CodigoBarra = pagamento.CodigoPagamento,
                    CodCliente = pagamento.CodigoCliente,
                    AgenciaCliente = pagamento.Agencia,
                    ContaCliente = pagamento.Conta,
                    Data = pagamento.DataPagamento,
                    IdentificacaoPagamento = pagamento.IdentificacaoPagamento,
                    Protocolo = pagamento.Protocolo,
                    CodigoCanal = 2
                };

                return await _apiIntegracao.CalcularTituloAsync(calcularRequest);
            }
            catch (Exception ex)
            {
                AddTrace(Issues.se3019, "Falha ao calcular pagamento", ex);
                throw;
            }
            finally
            {
                sw.Stop();
                AddTrace("Método api de calculo finalizado, tempo total gasto: {@InformationData}", sw.Elapsed);
            }
        }

        public async Task<bool> IncluirPagamentoTibcoAsync(IncluirPagamentoTibcoRequest pagamento)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                return await _apiIntegracao.IncluirPagamentoTibcoAsync(pagamento);
            }
            catch (Exception ex)
            {
                AddTrace(Issues.se3040, "Falha ao enviar o pagamento via tibco", ex);
                throw;
            }
            finally
            {
                sw.Stop();
                AddTrace("Método inclusão de pagto no Tibco finalizado, tempo total gasto: {@InformationData}", sw.Elapsed);
            }
        }

        public async Task<CoreComprovanteResponse> ObterComprovante(string protocolo)
        {
            var result = default(CoreComprovanteResponse);
            var sw = Stopwatch.StartNew();
            try
            {
                var request = new ComprovanteRequest();
                request.id.Add(protocolo);
                AddTrace("Efetuando request API Core comprovante por lote para consulta do comprovante");
                result = await _apiIntegracao.CoreObterComprovante(request);

                if (result?.Status != null)
                {
                    return result;
                }
                else
                {
                    AddTrace(string.Format(FriendlyMessages.ServiceErrorExternalApi, "Core Comprovante {@InformationData}"), result);
                }
            }
            catch (Exception ex)
            {
                AddError(Issues.se3010, string.Format(FriendlyMessages.ServiceErrorExternalApi, "Core Comprovante"), ex);
            }
            finally
            {
                sw.Stop();
                AddTrace("Tempo total gasto request API Core Comprovante {@InformationData}", sw.Elapsed);
            }
            return result;
        }

        
    }
}
