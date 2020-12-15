using ABCBrasil.OpenBanking.BackOfficeTed.Core.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Api.Common.Extensions
{
    public struct ApiInfos
    {
        public string ApiDescription { get; set; }
    }

    public static class SwaggerExtensions
    {
        static ApiInfos _apiInfos;

        public static void SetSwagger(this IServiceCollection services, ApiInfos apiInfos)
        {
            _apiInfos = apiInfos;
            services.SetApiVersion();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(config =>
            {
                config.TagActionsBy(api => api.GroupBySwaggerGroupAttribute());
                config.SetXmlDocumentation();
                config.SetSecurity();
            });
            services.AddSwaggerGenNewtonsoftSupport();
        }

        public static void SetSwagger(this IApplicationBuilder app, IApiVersionDescriptionProvider versionProvider)
        {
            app.UsePathBase($"/{AppDomain.CurrentDomain.FriendlyName.ToLower()}");

            app.UseSwagger(c =>
            {
                var openList = new List<OpenApiServer>();
                c.SerializeAsV2 = true;
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    foreach (var apiVersion in versionProvider.ApiVersionDescriptions)
                    {
                        c.RouteTemplate = $"swagger/v{apiVersion.ApiVersion}/swagger.json";
                    }
                    var pathBase = string.Empty;
#if !DEBUG
                    pathBase = $"/{AppDomain.CurrentDomain.FriendlyName.ToLower()}";
#endif
                    openList.Add(new OpenApiServer { Url = $"{httpReq.Scheme}://{httpReq.Host.Value}{pathBase}" });
                    swaggerDoc.Servers = openList;
                });
            });

            app.UseSwaggerUI(config =>
            {
                config.ConfigureSwaggerUI(versionProvider);
            });
        }

        static void SetXmlDocumentation(this SwaggerGenOptions options)
        {
            var xmlDocumentPath = GetXmlDocumentPath();
            var existsXmlDocument = File.Exists(xmlDocumentPath);
            if (existsXmlDocument)
            {
                options.IncludeXmlComments(xmlDocumentPath);
            }
        }
      

        static void SetSecurity(this SwaggerGenOptions options)
        {
            var configuration = AppConfiguration.GetConfiguration();
                
            var key = configuration["ABCBrasilApiSettings:keyName"];
            var value = configuration["ABCBrasilApiSettings:keyValue"];

            if (string.IsNullOrEmpty(key) || string.IsNullOrEmpty(value)) return;

            var securityScheme = new OpenApiSecurityScheme()
            {
                Description = value,
                Name = key,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = key
            };

            options.AddSecurityDefinition(key, securityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = key
                        }
                    }, new List<string>() }
            });
        }

        static void SetSecurityAnitCsrf(this SwaggerGenOptions options)
        {
            var scrf = "__ABC_ANTI_CSRF_LOGIN__";

            var csrfSecurityScheme = new OpenApiSecurityScheme
            {
                Name = scrf,
                In = ParameterLocation.Header,
                Description = scrf,
                Type = SecuritySchemeType.ApiKey,
                Scheme = scrf
            };
            options.AddSecurityDefinition(scrf, csrfSecurityScheme);
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = scrf
                        }
                    }, new List<string>()
                }
            });

        }

        static string GetXmlDocumentPath()
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            return Path.Combine(AppContext.BaseDirectory, xmlFile);
        }

        static IList<string> GroupBySwaggerGroupAttribute(this ApiDescription api)
        {
            var groupName = string.Empty;

            if (api.TryGetMethodInfo(out MethodInfo methodInfo))
            {
                var attribute = methodInfo.DeclaringType.CustomAttributes.SingleOrDefault(o => o.AttributeType.Name.Equals("SwaggerGroupAttribute"));
                groupName = attribute?.ConstructorArguments?.FirstOrDefault().Value?.ToString();
            }

            if (!string.IsNullOrEmpty(groupName))
            {
                return new List<string> { groupName };
            }
            else
            {
                var actionDescriptor = api.GetProperty<ControllerActionDescriptor>();
                if (actionDescriptor == null)
                {
                    actionDescriptor = api.ActionDescriptor as ControllerActionDescriptor;
                    api.SetProperty(actionDescriptor);
                }
                return new List<string> { actionDescriptor?.ControllerName };
            }
        }

        static void ConfigureSwaggerUI(this SwaggerUIOptions swaggerUIOptions, IApiVersionDescriptionProvider versionProvider)
        {

            foreach (var apiVersion in versionProvider.ApiVersionDescriptions)
            {
                swaggerUIOptions.SwaggerEndpoint($"/{AppDomain.CurrentDomain.FriendlyName.ToLower()}/swagger/v{apiVersion.ApiVersion}/swagger.json", $"{AppDomain.CurrentDomain.FriendlyName} - v{apiVersion.ApiVersion}");
            }

            swaggerUIOptions.RoutePrefix = "swagger";
            swaggerUIOptions.DocExpansion(DocExpansion.List);
        }

        public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
        {
            readonly IApiVersionDescriptionProvider _provider;
            public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => _provider = provider;

            public void Configure(SwaggerGenOptions options)
            {
                foreach (var description in _provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
                }
                options.OperationFilter<RemoveVersionFromParameter>();
                options.DocumentFilter<ReplaceVersionWithExactValueInPath>();
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.FirstOrDefault());
            }

            public static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
            {
                var build = Assembly.GetEntryAssembly().GetName().Version.ToString();
                var apiName = _apiInfos.ApiDescription;
                var info = new OpenApiInfo()
                {
                    Title = apiName,
                    Version = description.GroupName.ToString(),
                    Description = $"{apiName} - build: {build}",
                };

                info.Description += description.IsDeprecated ? $" - <strong style='color:red;'>Esta versão da API está descontinuada.</strong>" : string.Empty;

                return info;
            }
        }

    }
    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var paths = new OpenApiPaths();
            foreach (var path in swaggerDoc.Paths)
            {
                paths.Add(path.Key.Replace("v{version}", swaggerDoc.Info.Version), path.Value);
            }
            swaggerDoc.Paths = paths;
        }
    }
    public class RemoveVersionFromParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var versionParameter = operation.Parameters.SingleOrDefault(p => p.Name == "version");
            operation.Parameters.Remove(versionParameter);
        }
    }
    public sealed class SwaggerGroupAttribute : Attribute
    {
        public string GroupName { get; }
        public SwaggerGroupAttribute(string groupName) => GroupName = groupName;
    }
}
