using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Infra.Common
{
    public class ApiRequestHelper
    {
        readonly HttpClient _httpClient;

        public ApiRequestHelper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> PostAsync(
           BaseConfig baseConfig,
           string endpointApi,
           object objeto,
           string protocolo)
        {
            var uri = new UriBuilder($"{baseConfig.BaseAddress.TrimEnd('/')}{endpointApi}");

            Uri finalUrl = uri.Uri;
            string json = JsonConvert.SerializeObject(objeto);
            var httpConteudo = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add(Constants.CORRELATION_HEADER_KEY, protocolo);
            _httpClient.DefaultRequestHeaders.Add(baseConfig.ApiKeyName, baseConfig.ApiKey);
            return await _httpClient.PostAsync(finalUrl, httpConteudo);
        }
        public async Task<T> PostAsync<T>(
           BaseConfig baseConfig,
           string endpointApi,
           object objeto,
           string protocolo)
        {
            var uri = new UriBuilder($"{baseConfig.BaseAddress.TrimEnd('/')}{endpointApi}");

            Uri finalUrl = uri.Uri;
            string json = JsonConvert.SerializeObject(objeto);
            var httpConteudo = new StringContent(json, Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add(Constants.CORRELATION_HEADER_KEY, protocolo);
            _httpClient.DefaultRequestHeaders.Add(baseConfig.ApiKeyName, baseConfig.ApiKey);

            using (var response = await _httpClient.PostAsync(finalUrl, httpConteudo).ConfigureAwait(false))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(apiResponse))
                    return JsonConvert.DeserializeObject<T>(apiResponse);
                else
                    return default;
            }
        }

        public async Task<HttpResponseMessage> GetAsync(
            object param,
            BaseConfig baseConfig,
            string endpointApi,
            string protocolo)
        {
            var uriBuilder = new UriBuilder($"{baseConfig.BaseAddress.TrimEnd('/')}{endpointApi}");
            uriBuilder.Query = param.ToString();
            Uri finalUrl = uriBuilder.Uri;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add(baseConfig.ApiKeyName, baseConfig.ApiKey);
            _httpClient.DefaultRequestHeaders.Add(Constants.CORRELATION_HEADER_KEY, protocolo);

            return await _httpClient.GetAsync(finalUrl).ConfigureAwait(false);
        }
        public async Task<T> GetAsync<T>(
            object param,
            BaseConfig baseConfig,
            string endpointApi,
            string protocolo)
        {
            var uriBuilder = new UriBuilder($"{baseConfig.BaseAddress.TrimEnd('/')}{endpointApi}");
            uriBuilder.Query = param.ToString();
            Uri finalUrl = uriBuilder.Uri;

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add(baseConfig.ApiKeyName, baseConfig.ApiKey);
            _httpClient.DefaultRequestHeaders.Add(Constants.CORRELATION_HEADER_KEY, protocolo);

            using (var response = await _httpClient.GetAsync(finalUrl).ConfigureAwait(false))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(apiResponse))
                    return JsonConvert.DeserializeObject<T>(apiResponse);
                else
                    return default;
            }
        }
    }
}
