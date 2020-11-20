using ABCBrasil.LogEventos.Lib.Api.QueueSenders;
using ABCBrasil.LogEventos.Lib.Infra;
using ABCBrasil.LogEventos.Lib.Senders.Interfaces;
using ABCBrasil.LogEventos.Lib.Senders.QueueSenders;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common.Tracer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Infra.Repository;
using ABCBrasil.SegurancaApi.DSL.Libs.Interfaces;
using ABCBrasil.SegurancaApi.DSL.Libs.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using System;
using System.Net.Http;


namespace ABCBrasil.OpenBanking.BackOfficeTed.IoC
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection builder, IConfiguration configuration)
        {
            builder
                .AddServices(configuration)
                .AddRepository()
                .AddComponents();

            return builder;
        }

        static IServiceCollection AddServices(this IServiceCollection builder, IConfiguration configuration)
        {
            builder.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            builder.AddSingleton<IApiIssuer, ApiIssuer>();
            builder.AddScoped<ITedService, TedService>();
            builder.AddScoped<IAntiCSRFService, AntiCSRFService>();
            builder.AddScoped<IIBRepository, IBRepository>();
            //builder.AddScoped<ITraceHandler, TraceHandler>();

            //Log Eventos
            builder.AddScoped<ILogService, LogService>();
            builder.AddScoped<IRegistroEventoService, RegistroEventoService>();
            builder.AddScoped<ISenderLogEventos, QueueSenderLogEventos>();
            builder.AddScoped<IEventQueueSender, EventQueueSender>();




            return builder;
        }

        static IServiceCollection AddRepository(this IServiceCollection builder)
        {
            //---
            //Cache


            //---
            //Repositories
            builder.AddScoped<IParametrosRepository, ParametrosRepository>();
            builder.AddScoped<IEventoRepository, EventoRepository>();

            return builder;
        }

        static IServiceCollection AddComponents(this IServiceCollection builder)
        {
            builder.AddNotificationHandler();
            builder.AddTraceHandler();
            return builder;
        }

        public static IServiceCollection AddTraceHandler(this IServiceCollection builder)
        {
            builder.AddTransient<ILogService, LogService>();
            builder.AddTransient<ITraceHandler, TraceHandler>();
            return builder;
        }

    }
}