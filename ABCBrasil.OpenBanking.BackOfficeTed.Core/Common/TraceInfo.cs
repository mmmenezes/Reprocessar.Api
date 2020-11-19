using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common
{
    public class TraceInfo
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public object InformationData { get; set; }
        public Exception Exception { get; set; }
        public TraceLevel Level { get; set; } = TraceLevel.Information;
    }

    public enum TraceLevel { Information = 2, Warning = 3, Error = 4 }
}
