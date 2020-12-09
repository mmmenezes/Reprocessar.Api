
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.EventLog;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.BackOfficeTed.Tests.Unit;
using Castle.Windsor;
using Microsoft.AspNetCore.Http;
using Moq;


namespace ABCBrasil.OpenBanking.Pagamento.Tests.Unit.Controllers
{
    public class BaseControllerTests<TController>
    {
        public readonly IWindsorContainer _container;
        public TController _controllerBase;
        public readonly Mock<IApiIssuer> _issuer;
        public readonly Mock<IRegistroEventoService> _registroEventoService;
        public readonly Mock<INotificationHandler> _notificationHandler;
        public readonly Mock<ITraceHandler> _traceHandler;
        public readonly string _correlationId;


        public BaseControllerTests()
        {
            //_correlationId = Guid.NewGuid().ToString();
            _registroEventoService = new Mock<IRegistroEventoService>();
            _issuer = new Mock<IApiIssuer>();
            _notificationHandler = new Mock<INotificationHandler>();
            _traceHandler = new Mock<ITraceHandler>();

            _container = new WindsorContainer();
            _container.Install(new BaseInstaller<TController>());
            _controllerBase = _container.Resolve<TController>();

            SetInitSources();
        }

        private void SetInitSources()
        {
            _container.Resolve<Mock<ITraceHandler>>()
               .Setup(x => x.CorrelationId)
              .Returns(_correlationId);
        }


        public void ArrangeServiceIssuer(params Issues[] issues)
        {
            foreach (var issue in issues)
                Builder.ArrangeServiceIssuer(_container, issue);
        }

        public DefaultHttpContext SetHeaders()
        {
            var context = new DefaultHttpContext();
            context.HttpContext.Request.Headers.Add("X-Correlation-ID", _correlationId);
            context.HttpContext.Request.Headers.Add("X-CorrelationID", _correlationId);
            context.HttpContext.Request.Headers.Add("__ABC_ANTI_CSRF_LOGIN__", "Userboletoonline");
            context.HttpContext.Request.Headers.Add("ABC_KeyID", "174BABE4-8BA5-4FF1-A38F-CEB5F8CDFCC4");
            return context;
        }
    }
}