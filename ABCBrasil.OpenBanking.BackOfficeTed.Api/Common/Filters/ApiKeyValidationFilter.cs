
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Filters
{
    public class ApiKeyValidationFilter : IActionFilter
    {
        const string CONTENT_TYPE = "application/json";
        const string HEADER_INFOS = "Infos do Header";
        const string NOT_AUTHORIZED_PHRASE = "Acesso não autorizado. API Key não encontrada ou inválida.";
        const string API_KEY_NOT_CONFIGURED = "APIKey não configurado, contate o adminstrador do sistema";
        const string API_KEY_NOT_FOUND = "APIKey não encontrado";
        const string NOT_AUTHORIZED = "Não autorizado";
        const string AUTHORIZED = "Autorizado";
        const string MESSAGE_KEY = " - Validação da ApiKey.";

        readonly AbcBrasilApiSettings _setting;
        ActionExecutingContext _filterContext;
        readonly IApiIssuer _issuer;
        readonly ITraceHandler _traceHandler;

        public ApiKeyValidationFilter(IOptions<AbcBrasilApiSettings> setting, IApiIssuer issuer, ITraceHandler traceHandler)
        {
            _setting = setting?.Value;
            _issuer = issuer;

            _traceHandler = traceHandler;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            //No code
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            _filterContext = context;

            AddTrace(HEADER_INFOS, TraceLevel.Information, Issues.None,
                informationData: new
                {
                    data = JsonConvert.SerializeObject(_filterContext?.HttpContext.Request.Headers,
                    new JsonSerializerSettings
                    {
                        PreserveReferencesHandling = PreserveReferencesHandling.None
                    })
                });

            if (!IsAuthorized())
            {
                _filterContext.Result = new ContentResult
                {
                    Content = NOT_AUTHORIZED_PHRASE,
                    ContentType = CONTENT_TYPE,
                    StatusCode = (int)System.Net.HttpStatusCode.Unauthorized
                };
            }
        }

        bool IsAuthorized()
        {
            var keyName = _setting?.KeyName ?? string.Empty;
            var keyValue = _setting?.KeyValue ?? string.Empty;

            if (string.IsNullOrWhiteSpace(keyName) || string.IsNullOrWhiteSpace(keyValue))
            {
                AddTrace(API_KEY_NOT_CONFIGURED, TraceLevel.Error, Issues.fe1001);
                return false;
            }

            var keyIdValue = _filterContext?.HttpContext.Request.Headers[keyName];
            if (string.IsNullOrEmpty(keyIdValue))
            {
                AddTrace(API_KEY_NOT_FOUND, TraceLevel.Error, Issues.fe1002);
                return false;
            }

            var result = keyValue.Equals(keyIdValue);
            LogResult(result);
            return result;
        }

        void LogResult(bool result)
        {
            var message = string.Concat(!result ? NOT_AUTHORIZED : AUTHORIZED, MESSAGE_KEY);
            var level = !result ? TraceLevel.Error : TraceLevel.Information;
            var issue = !result ? Issues.fe1003 : Issues.None;

            AddTrace(message, level, issue);
        }

        void AddTrace(string message, TraceLevel level, Issues issue, object informationData = null)
        {
            try
            {
                _traceHandler?.AddTrace(new TraceInfo
                {
                    Message = message,
                    Level = level,
                    InformationData = informationData,
                    Code = issue != Issues.None ? _issuer.Maker(issue) : string.Empty
                }, _issuer.Prefix);
            }
            catch (Exception ex)
            {
                _traceHandler?.AddTrace(new TraceInfo
                {
                    Message = ex.Message,
                    Level = TraceLevel.Error,
                    Code = _issuer.Maker(Issues.fe1004),
                    Exception = ex
                }, _issuer.Prefix);
            }
        }
    }
}
