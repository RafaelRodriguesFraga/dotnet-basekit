using DotnetBaseKit.Components.Application.Base;
using DotnetBaseKit.Components.Application.Pagination;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetBaseKit.Components.Application
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
