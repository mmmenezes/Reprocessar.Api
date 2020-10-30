using ABCBrasil.IB.Corporate.Core.Dsl.Lib.Common;
using ABCBrasil.LogEventos.Lib.Queues.Objects.Payloads;
using ABCBrasil.LogEventos.Lib.Queues.Objects.Shared;
using ABCBrasil.LogEventos.Lib.Senders.Interfaces;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.SegurancaApi.DSL.Libs.Interfaces;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Services
{
    public class RegistroEventoService : ServiceBase, IRegistroEventoService
    {
        readonly ISenderLogEventos _senderLogEventos;
        readonly IAntiCSRFService _antiCSRFService;
        public RegistroEventoService(ISenderLogEventos senderLogEventos, IApiIssuer issuer, IAntiCSRFService antiCSRFService) : base(issuer)
        {
            _senderLogEventos = senderLogEventos;
            _antiCSRFService = antiCSRFService;
        }
        public Task IncluirEvento(string payload, bool status, HttpRequest httprequest, string errormessage)
        {
            return IncluirEvento(httprequest.GetCorrelationIdFromHeader(), payload, status, httprequest.Method, httprequest.Host.Value.ToString() + httprequest.Path.ToString(), errormessage);
        }
        public async Task IncluirEvento(string correlationid, string payload, bool status, string method, string endpoint, string errormessage)
        {
            var sw = Stopwatch.StartNew();
            try
            {
                AddTrace("Incluir Registro de Evento");

                _senderLogEventos.InserirLogEventos(MontaRegistroEvento(payload, status, method, endpoint, errormessage), correlationid);

                AddTrace("Registro de Eventos inserido com sucesso");
                await Task.CompletedTask;
            }
            catch (Exception ex)
            {
                AddTrace(Issues.se3015, Resources.FriendlyMessages.ErroServicoIncluirRegistroEvento, ex);
            }
            finally
            {
                sw.Stop();
                AddTrace($"Método de Incluir registro de evento finalizado, tempo total gasto {sw.Elapsed}");
            }
        }
        private RegistroEvento MontaRegistroEvento(string payload, bool status, string method, string endpoint, string descricao)
        {
            return new RegistroEvento()
            {
                LoginUsuario = _antiCSRFService?.Login ?? "",
                AppId = _antiCSRFService?.AppId ?? "",
                AppName = _antiCSRFService?.AppName ?? "",
                VerboHTTP = method,
                Endpoint = endpoint,
                PayloadRequest = string.IsNullOrEmpty(payload)? payload : string.Empty,
                Status = status ? StatusLogEventos.SUCESSO.ToString() : StatusLogEventos.ERRO.ToString(),
                DescricaoErro = descricao
            };
        }
    }
}
