using ABCBrasil.OpenBanking.Pagamento.Core.Common;
using ABCBrasil.OpenBanking.Pagamento.Core.Interfaces.ExternalApis;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.Calculo;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.Calendario;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CoreComprovante;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.CorePagamento;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.AbcBrasilApiIntegracao.Tibco;
using ABCBrasil.OpenBanking.Pagamento.Core.Models.Cip;
using ABCBrasil.OpenBanking.Pagamento.Core.ViewModels.Arguments.Comprovante;
using Newtonsoft.Json;
using ABCBrasil.OpenBanking.Pagamento.Infra.Common;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Linq;

namespace ABCBrasil.OpenBanking.Pagamento.Infra.ExternalApis
{
    public class AbcBrasilApiIntegracao : ApiRequestHelper, IAbcBrasilApiIntegracao
    {
        CalendarioConfig _calendarioConfig;
        CoreCalculoConfig _coreCalculoConfig;
        CorePagamentoLoteConfig _corePagamentoLoteConfig;
        CoreComprovanteConfig _coreComprovanteConfig;
        TibcoPagamentoConfig _tibcoPagamentoConfig;

        public AbcBrasilApiIntegracao(HttpClient httpClient):base(httpClient)
        {
        }

        public void SetConfig(AbcBrasilApiSettings abcBrasilApiSettings)
        {
            _calendarioConfig = abcBrasilApiSettings?.CalendarioConfig;
            _coreCalculoConfig = abcBrasilApiSettings?.CoreCalculoConfig;
            _corePagamentoLoteConfig = abcBrasilApiSettings?.CorePagamentoLoteConfig;
            _tibcoPagamentoConfig = abcBrasilApiSettings?.TibcoPagamentoConfig;
            _coreComprovanteConfig = abcBrasilApiSettings?.CoreComprovanteConfig;
        }

        public async Task<DiaUtilResponse> ConsultarDiaUtil(DiaUtilRequest dataHora, string protocolo)
        {
            return await base.PostAsync<DiaUtilResponse>(_calendarioConfig, UrlIntegracao.ENDPOINT_DIAUTIL, dataHora, protocolo);
        }

        public async Task<DataExcecaoResponse> ConsultarDataExcecao(DataExcecaoRequest dataExcecao, string protocolo)
        {
            return await base.PostAsync<DataExcecaoResponse>(_calendarioConfig, UrlIntegracao.ENDPOINT_DATAEXCECAO, dataExcecao, protocolo);
        }

        //
        // todo Gameiro: Canal e transação mocados, pendente de inclusão na base do calendário (nova sprint)
        public async Task<HoraTransacaoResponse> ConsultarHoraTransacao(Core.Models.Pagamento pagamento)
        {
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["CodigoTransacional"] = pagamento.TipoPagamento.ToString();

            return await base.GetAsync<HoraTransacaoResponse>(parameters, _calendarioConfig, string.Format(UrlIntegracao.ENDPOINT_HORATRANSACAO, "0"), pagamento.Protocolo.ToString());

        }

        /// <summary>
        /// Método responsável por consultar na api de calendário o próximo dia útil
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="correlationId">CorrelationId gerado na api de pagamento</param>
        /// <returns>Objeto payload response DateResult</returns>
        public async Task<AbcServiceResult<DateResult>> ObterProximoDiaUtil(DateTime data, string correlationId)
        {
            //TODO:VERIFICAR SE REALMENTE É NECESSARIO PASSAR O PROTOCOLO.
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["Data"] = data.ToString("dd-MM-yyyy");
            parameters["Moeda"] = "0";
            parameters["TipoFeriado"] = "SP";

            return await base.GetAsync<AbcServiceResult<DateResult>>(parameters, _calendarioConfig, UrlIntegracao.PAHT_CALENDARIO, correlationId).ConfigureAwait(false);
        
        }

        
        /// <summary>
        /// Método responsável por consultar código de barras na CIP via Api Core CIP
        /// </summary>
        /// <param name="codigoDeBarras">código de barras válidado</param>
        /// <param name="correlationId">CorrelationId gerado na api de pagamento</param>
        /// <returns>Payload response com dados do boleto</returns>
        public async Task<BoletoCipResult> ObterBoletoAsync(string codigoDeBarras, string correlationId)
        {
            ///return Mocks.BoletoCipMock.Get();
            return await base.GetAsync<BoletoCipResult>(null, _coreCalculoConfig, $"{UrlIntegracao.PAHT_BOLETO_CIP}{codigoDeBarras}", correlationId);
        }

        public async Task<CalculaTituloResponse> CalcularTituloAsync(CalculaTituloRequest calcularRequest)
        {
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["Data"] = calcularRequest.Data.ToString("yyyy-MM-dd");
            parameters["Protocolo"] = calcularRequest.Protocolo.ToString();
            parameters["CodCliente"] = calcularRequest.CodCliente.ToString();
            parameters["AgenciaCliente"] = calcularRequest.AgenciaCliente;
            parameters["ContaCliente"] = calcularRequest.ContaCliente;
            parameters["IdentificacaoPagamento"] = calcularRequest.IdentificacaoPagamento;
            parameters["CodigoCanal"] = calcularRequest.CodigoCanal.ToString();

            return await base.GetAsync<CalculaTituloResponse>(parameters, _coreCalculoConfig, string.Format(UrlIntegracao.ENDPOINT_CALCULO, calcularRequest.CodigoBarra), calcularRequest.Protocolo.ToString());
        }

        public async Task<bool> IncluirPagamentoTibcoAsync(IncluirPagamentoTibcoRequest pagamento)
        {
            var resultado = await base.PostAsync(_tibcoPagamentoConfig, UrlIntegracao.ENDPOINT_TIBCO, pagamento, pagamento.Protocolo);

            return resultado.IsSuccessStatusCode;
        }

        public async Task<CoreComprovanteResponse> CoreObterComprovante(ComprovanteRequest protocolo)
        {
            var resultado = await base.PostAsync<CoreComprovanteResponse>(_coreComprovanteConfig, UrlIntegracao.ENDPOINT_COMPROVANTE, protocolo, protocolo.id.FirstOrDefault());
            return resultado;
            //if (resultado.IsSuccessStatusCode)
            //{
            //    var objetoResponse = await resultado.Content.ReadAsStringAsync();
            //    return JsonConvert.DeserializeObject<CoreComprovanteResponse>(objetoResponse);
            //}
            //else
            //{
            //    CoreComprovanteResponse.CoreComprovanteResponseErrors erros = new CoreComprovanteResponse.CoreComprovanteResponseErrors() { Code = resultado.StatusCode.ToString(), Message = "Erro ao requisitar api - Comprovante em lote ." };



            //    var retornoErro = new CoreComprovanteResponse() { Status = false };
            //    retornoErro.Errors.Add(erros);
            //    return retornoErro;
            //}

        }       
    }
}
