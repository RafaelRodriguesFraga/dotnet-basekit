using DotnetBaseKit.Components.Api.Responses;
using DotnetBaseKit.Components.Shared.Notifications;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetBaseKit.Components.Api
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services, bool includeKey = true)
        {
            services.AddScoped<NotificationContext>();

            services.AddScoped<INotificationMessageFormatter, NotificationMessageFormatter>();
            services.AddScoped<IResponseFactory>(sp =>
                new ResponseFactory(
                    sp.GetRequiredService<NotificationContext>(),
                    sp.GetRequiredService<INotificationMessageFormatter>(),
                    includeKey));

            return services;
        }
    }
}
