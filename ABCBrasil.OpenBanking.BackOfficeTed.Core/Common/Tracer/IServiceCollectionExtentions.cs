
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common.Tracer
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddTraceHandler(this IServiceCollection builder)
        {
            builder.AddTransient<ILogService, LogService>();
            builder.AddTransient<ITraceHandler, TraceHandler>();
            return builder;
        }
    }
}
