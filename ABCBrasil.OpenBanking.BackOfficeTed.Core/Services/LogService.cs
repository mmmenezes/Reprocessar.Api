
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog.Context;
using Serilog.Events;
using System;
using System.Collections.Generic;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class LogService : ILogService
    {
        readonly bool _isNotEncrypt;

        public LogService()
        {
            _isNotEncrypt = IsNotEncrypt();
        }

        public string CorrelationId { get; set; }

        public void Write(IssueDetail issue, string prefix = "")
        {
            var levelId = (int)issue.Level;
            var level = (LogEventLevel)levelId;
            var protocol = MakeProtocol(issue);

            WriteLog(level, $"{prefix} - {protocol} {issue.Message}", issue.Exception, issue.Date, issue.InformationData);
        }

        private string MakeProtocol(IssueDetail issue)
        {
            var issueId = !string.IsNullOrEmpty(issue?.Id) ? $"[Id:{issue.Id}]" : string.Empty;
            var issueCode = !string.IsNullOrEmpty(issue?.Code) && issue?.Id?.IndexOf(issue?.Code) < 0 ? $"[Code:{issue.Code}]" : string.Empty;
            var result = $"{issueId}.{issueCode}".Trim('.');

            return !string.IsNullOrEmpty(result) ? $"{result} - " : string.Empty;
        }

        public void Write(IList<IssueDetail> issues, string prefix = "")
        {
            foreach (var issue in issues)
            {
                var levelId = (int)issue.Level;
                var level = (LogEventLevel)levelId;
                var protocol = MakeProtocol(issue);

                WriteLog(level, $"{prefix} - {protocol} {issue.Message}", issue.Exception, issue.Date, issue.InformationData);
            }
        }

        public void LogInfo(string message, object data = null)
        {
            WriteLog(LogEventLevel.Information, message, null, DateTime.Now, data);
        }

        public void LogWarning(string message, Exception exception = null, object data = null)
        {
            WriteLog(LogEventLevel.Warning, message, exception, DateTime.Now, data);
        }

        public void LogError(string message, Exception exception = null, object data = null)
        {
            WriteLog(LogEventLevel.Error, message, exception, DateTime.Now, data);
        }

        void WriteLog(LogEventLevel level, string message, Exception exception = null, DateTime? date = null, object data = null)
        {
            using (LogContext.PushProperty(Constants.CORRELATION_HEADER_KEY, CorrelationId))
            {

                var content = GetContent(data);

                if (date.HasValue)
                    LogContext.PushProperty("Moment", date);

                if (data != null)
                    LogContext.PushProperty("InformationData", content);

                Serilog.Log.Write(level, exception, message, content);
            }
        }

        string GetContent(object data)
        {
            if (data == null) return string.Empty;

            var result = JsonConvert.SerializeObject(data);
            var key = CorrelationId;
            if (_isNotEncrypt)
            {
                try
                {
                    result = ABCBrasil.Core.Seguranca.CryptoLegado.Encrypt(result, key);
                }
                catch { }
            }
            return result;
        }

        bool IsNotEncrypt()
        {
            var configuration = AppConfiguration.GetConfiguration();
            return configuration.GetValue<bool>("Serilog:IsNotEncrypt");
        }
    }
}
