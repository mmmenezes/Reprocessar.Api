
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common.Tracer
{
    public class TraceHandler : ITraceHandler
    {
        readonly ILogService _logService;
        public TraceHandler(ILogService logService) => _logService = logService;

        public string CorrelationId { get; set; }
            

        public async Task AddTrace(Common.TraceInfo trace, string prefix = "")
        {
            await Task.Run(() =>
            {
                _logService.CorrelationId = CorrelationId;
                _logService?.Write(new IssueDetail
                {
                    Id = trace.Code,
                    Message = trace.Message,
                    Date = trace.Date,
                    Exception = trace.Exception,
                    InformationData = trace.InformationData,
                    Level = (IssueLevelEnum)(int)trace.Level,
                }, prefix);
            });
        }
    }
}
