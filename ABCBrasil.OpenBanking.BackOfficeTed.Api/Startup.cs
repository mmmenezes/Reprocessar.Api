using ABCBrasil.Core;
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions;
using ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Filters;
using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api
{
    public class Startup
    {
        public static IConfiguration Config { get; set; }
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbConnect(Shared.Configuration.ABC_API);
            services.AddExtentions(Configuration);
            services.AddControllers()
                .AddNewtonsoftJson(NewtonsoftJsonExtension.ActionNewtonsoftJson);
            services
                .AddMvc(options =>
                {
                    options.Filters.Add(typeof(ApiKeyValidationFilter));
                });
            services.AddSwaggerGenNewtonsoftSupport();
            }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider versionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.AddExtensions(versionProvider);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseEndpoints(HealthCheckExtensions.ActionEndpontins);
        }
    }
}
