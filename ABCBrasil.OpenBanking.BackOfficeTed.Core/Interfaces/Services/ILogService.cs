
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Componente;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services
{
    public interface ILogService
    {
        string CorrelationId { get; set; }

        void Write(IssueDetail issue, string prefix = "");

        void Write(IList<IssueDetail> issues, string prefix = "");

        void LogInfo(string message, object data = null);

        void LogWarning(string message, Exception exception = null, object data = null);

        void LogError(string message, Exception exception = null, object data = null);

        void SetNoticationHandle(INotificationHandler notificationHandler);
        void SetTraceHandle(ITraceHandler traceHandler);
        Task IncluirLog(LogModel model);
    }
}
