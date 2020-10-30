
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Notification;
using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Tracer;
using ABCBrasil.LogEventos.Lib.Api.QueueSenders;
using ABCBrasil.LogEventos.Lib.Infra;
using ABCBrasil.LogEventos.Lib.Senders.Interfaces;
using ABCBrasil.LogEventos.Lib.Senders.QueueSenders;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Cache;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.ExternalApis;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Pagamento;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Infra.Cache;
using ABCBrasil.OpenBanking.BackOfficeTed.Infra.ExternalApis;
using ABCBrasil.OpenBanking.BackOfficeTed.Infra.Repository;
using ABCBrasil.SegurancaApi.DSL.Libs.Interfaces;
using ABCBrasil.SegurancaApi.DSL.Libs.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Retry;
using System;
using System.Net.Http;

namespace ABCBrasil.OpenBanking.BackOfficeTed.IoC
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection builder)
        {
            builder
                .AddServices()
                .AddRepository()
                .AddComponents();

            return builder;
        }

        static IServiceCollection AddServices(this IServiceCollection builder)
        {
            builder.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            builder.AddSingleton<IApiIssuer, ApiIssuer>();
            builder.AddScoped<IClientService, ClientService>();
            builder.AddScoped<Core.Interfaces.Services.ICipService, Core.Services.CipService>();
            builder.AddScoped<IPagamentoService, PagamentoService>();
            //builder.AddScoped<IPagamentoValida, PagamentoValida>();
            builder.AddScoped<IAntiCSRFService, AntiCSRFService>();
            builder.AddScoped<IComprovanteService, ComprovanteService>();

            //#HTTPClient
            builder.AddHttpClient<IAbcBrasilApiIntegracao, AbcBrasilApiIntegracao>()
                .AddPolicyHandler(GetPolicyRetry())
                //.AddPolicyHandler(GetPolicyCircuitBreaker())
                ;

            builder.AddHttpClient<IAbcBrasilNotificacaoIntegracao, AbcBrasilNotificacaoIntegracao>();
            builder.AddScoped<ICallbackTibcoService, CallbackTibcoService>();

            builder.AddScoped<IAbcBrasilNotificacaoIntegracaoService, AbcBrasilNotificacaoIntegracaoService>();
            builder.AddScoped<IAbcBrasilApiIntegracaoService, AbcBrasilApiIntegracaoService>();


            //Log Eventos
            builder.AddScoped<IRegistroEventoService, RegistroEventoService>();
            builder.AddScoped<ISenderLogEventos, QueueSenderLogEventos>();
            builder.AddScoped<IEventQueueSender, EventQueueSender>();


            //callback
            builder.AddScoped<ICallbackCoreService, CallbackCoreService>();
            

            return builder;
        }

        static IServiceCollection AddRepository(this IServiceCollection builder)
        {
            //---
            //Cache
            builder.AddScoped<IClientCache, ClientCache>();
            builder.AddScoped<ICipCache, CoreCipCache>();

            //---
            //Repositories
            builder.AddScoped<IParametrosRepository, ParametrosRepository>();
            builder.AddScoped<IClientRepository, ClientRepository>();
            builder.AddScoped<IPagamentoRepository, PagamentoRepository>();
            builder.AddScoped<ICipRepository, CipRepository>();
            builder.AddScoped<ICallbackCoreRepository, CallbackCoreRepository>();

            return builder;
        }

        static IServiceCollection AddComponents(this IServiceCollection builder)
        {
            builder.AddNotificationHandler();
            builder.AddTraceHandler();

            return builder;
        }

        private static AsyncRetryPolicy<HttpResponseMessage> GetPolicyRetry()
        {
            return Polly.Extensions.Http.HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(2),
                                     onRetry: (message, retryCount) =>
                                     {
                                         string msg = $"Retentativa: --{retryCount} --";
                                         Serilog.Log.Warning(msg);
                                         Serilog.Log.Warning(message.Exception.ToString());
                                         Console.Out.WriteLineAsync(msg);
                                     });
        }
        private static IAsyncPolicy<HttpResponseMessage> GetPolicyCircuitBreaker()
        {
            return Polly.Extensions.Http.HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(3, TimeSpan.FromSeconds(20),
                                     onBreak: (message, time) =>
                                     {
                                         string msg = $"CircuitBreaker aberto por: --{time} --";
                                         Serilog.Log.Warning(msg);
                                         Console.Out.WriteLineAsync(msg);
                                     },
                                     onReset: () =>
                                     {
                                         string msg = "Circuito fechado, solicitações fluem normalmente.";
                                         Serilog.Log.Warning(msg);
                                         Console.Out.WriteLineAsync(msg);
                                     },
                                     onHalfOpen: () =>
                                     {
                                         string msg = "Teste do Circuito, uma solicitação será permitida.";
                                         Serilog.Log.Warning(msg);
                                         Console.Out.WriteLineAsync(msg);
                                     });
        }
    }
}