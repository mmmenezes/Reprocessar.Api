using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using System.IO.Compression;

namespace ABCBrasil.OpenBanking.Pagamento.Api.Common.Extensions
{
    public static class ResponseCompressionExtension
    {
        public static void SetCompression(this IServiceCollection builder)
        {
            builder.Configure<GzipCompressionProviderOptions>(options => options.Level = CompressionLevel.Optimal);
            builder.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
            });
        }

        public static void UseCompression(this IApplicationBuilder app) =>
            app.UseResponseCompression();
    }
}
