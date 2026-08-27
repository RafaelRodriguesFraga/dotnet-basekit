
using Microsoft.Extensions.DependencyInjection;
using TestApi.Domain.Repositories;

namespace TestApi.Infra.CrossCutting.IoC
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<ITestApiWriteRepository, TestApiWriteRepository>();
            services.AddScoped<ITestApiSqlWriteRepository, TestApiSqlWriteRepository>();
            services.AddScoped<ITestApiSqlReadRepository, TestApiSqlReadRepository>();

            return services;
        }
    }
}
