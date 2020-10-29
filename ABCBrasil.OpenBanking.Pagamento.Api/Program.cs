using ABCBrasil.Log.Lib.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ABCBrasil.OpenBanking.Pagamento.Api
{
#pragma warning disable CS1591
    public class Program
    {
        protected Program() { }
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseSerilogSetup<Program>();
                    webBuilder.UseIISIntegration();
                    //webBuilder.UseKestrel();
                });
    }
#pragma warning restore CS1591
}
