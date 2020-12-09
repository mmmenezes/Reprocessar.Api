using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ABCBrasil.OpenBanking.Pagamento.Tests.Unit
{
    public static class SettingsTests
    {
        #region Set Config System
        private static object _thisLock = new object();
        private static bool _initialized = false;
        private static IConfigurationRoot _configuration;
        internal static IConfiguration SetConfiguration(this IServiceCollection services)
        {
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            services.AddSingleton<IConfiguration>(_configuration);

            lock (_thisLock)
            {
                if (!_initialized)
                {
                    _initialized = true;
                }
            }
            return _configuration;
        }
        #endregion
    }
}
