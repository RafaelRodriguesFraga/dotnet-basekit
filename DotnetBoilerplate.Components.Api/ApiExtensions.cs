using DotnetBoilerplate.Components.Api.Responses;
using DotnetBoilerplate.Components.Shared.Notifications;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetBoilerplate.Components.Api
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
