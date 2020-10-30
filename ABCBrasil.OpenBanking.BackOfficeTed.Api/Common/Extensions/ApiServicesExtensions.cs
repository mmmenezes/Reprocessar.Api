using ABCBrasil.Core.CacheRedis.Lib;
using ABCBrasil.Log.Lib.Extensions;
using ABCBrasil.LogEventos.Lib.Config;
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Middleware;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.IoC;
using ABCBrasil.SegurancaApi.DSL.Libs.Middleware;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core.Configuration;
using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions
{
    internal static class ApiServicesExtensions
    {
        internal static void AddExtentions(this IServiceCollection builder, IConfiguration configuration)
        {
            var redisConfiguration = configuration.GetSection("Redis").Get<RedisConfiguration>();
            builder.Configure<RedisConfiguration>(configuration.GetSection("Redis"));
            builder.Configure<AbcBrasilApiSettings>(configuration.GetSection("ABCBrasilApiSettings"));
            builder.Configure<LogEventosSettings>(configuration.GetSection("LogEventos"));
            builder.Configure<PagamentoConfig>(configuration.GetSection("PagamentoConfig"));
            builder.Configure<CalendarConfig>(configuration.GetSection("CalendarioConfig"));
            builder.Configure<TibcoPagamentoConfig>(configuration.GetSection("TibcoInclusaoPagamentoConfig"));

            builder.AddDependencies();

            var infos = new ApiInfos { ApiDescription = $"{Resources.ApiInfos.BankName} - {Resources.ApiInfos.ApiName}" };
            builder.SetSwagger(infos);
            builder.SetHealthChecks(redisConfiguration?.ConfigurationOptions?.ToString());
            builder.SetCompression();
            builder.AddSerilog();
            builder.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.AddCoreRedisCache(redisConfiguration);
        }

        internal static void AddExtensions(this IApplicationBuilder app, IApiVersionDescriptionProvider versionProvider)
        {
            app.SetSwagger(versionProvider);
            app.UseSerilog();
            app.UseCompression();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseMiddleware<AntiCSRFMiddleware>();
        }
    }
}

