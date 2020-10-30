using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions
{
    internal static class ApiVersionExtensions
    {
        internal static IServiceCollection SetApiVersion(this IServiceCollection builder)
        {
            builder.AddVersionedApiExplorer(p =>
            {
                p.GroupNameFormat = "'v'VVV";
                p.SubstituteApiVersionInUrl = true;
            });

            builder.AddApiVersioning(o =>
            {
                o.ReportApiVersions = true;
                o.AssumeDefaultVersionWhenUnspecified = true;
                o.DefaultApiVersion = new ApiVersion(1, 0);
                o.ApiVersionReader = new MediaTypeApiVersionReader();
            });

            return builder;
        }
    }
}
