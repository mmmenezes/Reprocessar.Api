
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Interfaces.Services;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Issuer;
using ABCBrasil.OpenBanking.Pagamento.Tests.Unit;
using AutoMapper;
using Castle.Windsor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using System;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Tests.Unit.Services
{
    public class BaseServiceTests<TService>
    {
        public readonly IWindsorContainer _container;
        public readonly TService _serviceBase;
        public readonly IConfiguration _config;
        public readonly Mock<IApiIssuer> _issuer;
        public readonly Mock<ITedService> _iTedService;
        public readonly Mock<IMapper> _mapper;
        public readonly Mock<INotificationHandler> _notificationHandler;
        public readonly Mock<ITraceHandler> _traceHandler;
        public readonly string _correlationId;
        public ServiceCollection _servicesCollections { get; }
        public readonly ServiceProvider _servicesProvider;
        

        public BaseServiceTests()
        {
            _servicesCollections = new ServiceCollection();
            _servicesCollections.SetConfiguration();
            _servicesProvider = _servicesCollections.BuildServiceProvider();

            _config = _servicesProvider.GetService<IConfiguration>();
           

            _issuer = new Mock<IApiIssuer>();
            _iTedService = new Mock<ITedService>();
            _mapper = new Mock<IMapper>();
            _notificationHandler = new Mock<INotificationHandler>();
            _traceHandler = new Mock<ITraceHandler>();
            _correlationId = Guid.NewGuid().ToString();

            _container = new WindsorContainer();

            _container.Install(new BaseInstaller<TService>());
            SetInitSources();
            _serviceBase = _container.Resolve<TService>();
        }
        
        private void SetInitSources()
        {
            var abcBrasilApiSettings = _config.GetSection("ABCBrasilApiSettings").Get<AbcBrasilApiSettings>();
            _container.Resolve<Mock<IOptions<AbcBrasilApiSettings>>>()
            .Setup(x => x.Value)
            .Returns(abcBrasilApiSettings);

            _container.Resolve<Mock<ITraceHandler>>().Setup(x => x.CorrelationId).Returns(_correlationId);
        }
        public void ArrangeServiceIssuer(params Issues[] issues)
        {
            foreach (var issue in issues)
                Builder.ArrangeServiceIssuer(_container, issue);
        }
    }
}
