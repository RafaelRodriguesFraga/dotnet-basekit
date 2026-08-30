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
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);

            var selectedDatabase = configuration["SelectedDatabase"];

            switch (selectedDatabase)
            {
                case "MySql":
                    var connectionString = GetRequiredConnectionString(configuration, "MysqlConnection");
                    var serverVersion = ServerVersion.AutoDetect(connectionString);

                    services.AddDbContext<TContext>(options => options.UseMySql(connectionString, serverVersion, optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(TContext).Assembly.FullName)));
                    break;

                case "SqlServer":
                    connectionString = GetRequiredConnectionString(configuration, "SqlServerConnection");

                    services.AddDbContext<TContext>(options => options.UseSqlServer(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(TContext).Assembly.FullName)));
                    break;

                case "Postgres":
                    connectionString = GetRequiredConnectionString(configuration, "PostgresConnection");

                    services.AddDbContext<TContext>(options => options.UseNpgsql(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(TContext).Assembly.FullName)));
                    break;

                case "SqLite":
                    connectionString = GetRequiredConnectionString(configuration, "SqliteConnection");

                    services.AddDbContext<TContext>(options => options.UseSqlite(connectionString, optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(TContext).Assembly.FullName)));
                    break;

                default:
                    throw new NotSupportedException($"Selected database '{selectedDatabase ?? "(not configured)"}' is not supported.");
            }

            AddSqlRepository(services);
            services.AddScoped<BaseContext, TContext>();

            return services;

        }

        public static IServiceCollection AddSqlRepository(IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);
            services.AddScoped(typeof(IBaseWriteRepository<>), typeof(BaseWriteRepository<>));
            services.AddScoped(typeof(IBaseReadRepository<>), typeof(BaseReadRepository<>));

            return services;
        }

        private static string GetRequiredConnectionString(IConfiguration configuration, string name)
        {
            return configuration.GetConnectionString(name)
                ?? throw new InvalidOperationException($"Connection string '{name}' is not configured.");
        }
    }
}
