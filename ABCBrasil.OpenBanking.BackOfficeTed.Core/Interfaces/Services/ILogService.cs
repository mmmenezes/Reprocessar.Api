
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using System;
using System.Collections.Generic;

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
    }
}
