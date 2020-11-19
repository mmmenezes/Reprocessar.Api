
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Models.Repository;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using ABCBrasil.SegurancaApi.DSL.Libs.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Controllers
{
    [ApiVersion("1")]
    [SwaggerGroup("ReprocessarTed")]
    [ApiController, Route("api/v{version:apiVersion}/reprocessar"), Produces("application/json")]
    public class ReprocessarController : RegistroEventoController
    {
        private readonly ITedService _tedService;
        readonly ITraceHandler _traceHandler;
       
        readonly IApiIssuer _issuer;
        readonly IAntiCSRFService _antiCSRFService;
        public ReprocessarController(IRegistroEventoService registroEventoService, IApiIssuer issuer, INotificationHandler notificationHandler, ITraceHandler traceHandler,ITedService tedService) : base(registroEventoService, issuer, notificationHandler, traceHandler)
        {
            _tedService = tedService;
            _traceHandler = traceHandler;
           
            _issuer = issuer;
        }

        //[ProducesResponseType(typeof(ApiResult<ComprovanteResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResult), StatusCodes.Status400BadRequest)]
        [HttpGet( Name = "PopulaTabela")]
        public async Task<IActionResult> PopulaTabela(BuscaTedRequest tedRequest)
        {
            var teds = _tedService.BuscaTeds(tedRequest);

            return Response<string>(teds, System.Net.HttpStatusCode.OK);
        }
    }
}
