using DotnetBoilerplate.Components.Application.Base;
using DotnetBoilerplate.Components.Application.Pagination;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetBoilerplate.Components.Application
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPaginationResponse<>), typeof(PaginationResponse<>));
            services.AddScoped<IBaseServiceApplication, BaseServiceApplication>();

            return services;
        }       
    }
}
