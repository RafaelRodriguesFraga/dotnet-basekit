using DotnetBaseKit.Components.Domain.Sql.Repositories;
using DotnetBaseKit.Components.Infra.Sql.Context.Base;
using DotnetBaseKit.Components.Infra.Sql.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetBaseKit.Components.Infra.Sql
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddDbContext<TContext>(this IServiceCollection services, IConfiguration configuration) where TContext : BaseContext
        {
            var selectedDatabase = configuration.GetSection("SelectedDatabase").Value;
            string connectionString = "";

            switch (selectedDatabase)
            {
                case "MySql":
                    connectionString = configuration.GetConnectionString("MysqlConnection");
                    var serverVersion = ServerVersion.AutoDetect(connectionString);

                    services.AddDbContext<TContext>(options => options.UseMySql(connectionString, serverVersion, optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(TContext).Assembly.FullName)));
                    break;

                case "SqlServer":
                    connectionString = configuration.GetConnectionString("SqlServerConnection");

                    services.AddDbContext<TContext>(options => options.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(TContext).Assembly.FullName)));
                    break;

                case "Postgres":
                    connectionString = configuration.GetConnectionString("PostgresConnection");

                    services.AddDbContext<TContext>(options => options.UseNpgsql(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(TContext).Assembly.FullName)));
                    break;

                default:
                    throw new NotSupportedException("Selected database is not supported.");
            }

            AddSqlRepository(services);
            services.AddScoped<BaseContext, TContext>();

            return services;

        }

        public static IServiceCollection AddSqlRepository(IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseWriteRepository<>), typeof(BaseWriteRepository<>));
            services.AddScoped(typeof(IBaseReadRepository<>), typeof(BaseReadRepository<>));

            return services;
        }
    }
}
