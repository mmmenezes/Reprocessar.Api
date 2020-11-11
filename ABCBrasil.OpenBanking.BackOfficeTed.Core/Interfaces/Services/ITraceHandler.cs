using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services
{
    public interface ITraceHandler
    {
        string CorrelationId { get; set; }

        Task AddTrace(TraceInfo trace, string prefix = "");
    }
}
