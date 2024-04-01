using DotnetBaseKit.Components.Api.Responses;
using DotnetBaseKit.Components.Shared.Notifications;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetBaseKit.Components.Api
{
    public static class ApiExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddScoped<IResponseFactory, ResponseFactory>();
            services.AddScoped<NotificationContext>();

            return services;
        }
    }
}
