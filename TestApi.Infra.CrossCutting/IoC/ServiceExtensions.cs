using Microsoft.Extensions.DependencyInjection;
using TestApi.Application.Services;

namespace TestApi.Infra.CrossCutting.IoC
{
    public static class ServiceExtensions
    {

        public static IServiceCollection AddServiceApplication(this IServiceCollection services)
        {
            services.AddScoped<ITestApiServiceApplication, TestApiServiceApplication>();

            return services;
        }
    }
}
