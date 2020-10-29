using ABCBrasil.Core.Data;
using ABCBrasil.OpenBanking.Pagamento.Core.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ABCBrasil.OpenBanking.Pagamento.Api.Common.Extensions
{
    public static class HealthCheckExtensions
    {
        public static void SetHealthChecks(this IServiceCollection builder, string redisConnection)
        {
            try
            {
                var conn = DBConnect.GetConnectionString(Shared.Configuration.ABC_CS_NAME);

                builder
                    .AddHealthChecks()
                    .AddSqlServer(conn, name: "SQL-TEMPLATE")
                    .AddRedis(redisConnection, name: "REDIS");
            }
            catch (Exception ex)
            {
                WriterLog(ex);
            }
        }

        internal static void ActionEndpontins(IEndpointRouteBuilder config)
        {
            try
            {
                config.MapHealthChecks("/healthchecks", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
            }
            catch (Exception ex)
            {
                WriterLog(ex);
            }

            config.MapDefaultControllerRoute();
        }

        static void WriterLog(Exception ex) =>
            Serilog.Log.Error(ex, "Falha ao carregar healthcheck");
    }
}
