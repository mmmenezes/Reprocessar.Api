using ABCBrasil.OpenBanking.BackOfficeTed.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ABCBrasil.OpenBanking.BackOfficeTed.Core.Common
{
    public static class IServiceCollectionExtentions
    {
        public static IServiceCollection AddNotificationHandler(this IServiceCollection builder)
        {
            builder.AddScoped<INotificationHandler, NotificationHandler>();
            return builder;
        }
    }
}
