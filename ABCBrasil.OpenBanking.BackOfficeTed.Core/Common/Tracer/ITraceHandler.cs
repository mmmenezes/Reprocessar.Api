using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common.Tracer
{
    public interface ITraceHandler
    {
        string CorrelationId { get; set; }

        Task AddTrace(TraceInfo trace, string prefix = "");
    }
}
