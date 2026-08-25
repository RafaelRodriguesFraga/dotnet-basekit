using DotnetBaseKit.Components.Domain.MongoDb.Repositories.Base;
using DotnetBaseKit.Components.Infra.MongoDb.DbSettings;
using DotnetBaseKit.Components.Infra.MongoDb.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace DotnetBaseKit.Components.Infra.MongoDb
{
    public static class MongoExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configuration);

            BsonSerializer.TryRegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));
            services.AddSingleton<IMongoSettings>(serviceProvider =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<MongoSettings>>().Value;
                ValidateSettings(settings);
                return settings;
            });
            services.AddSingleton<IMongoClient>(sp => new MongoClient(sp.GetRequiredService<IMongoSettings>().ConnectionString));
            services.AddScoped<DbSession>();
            AddRepositories(services);
            return services;
        }

        private static void ValidateSettings(IMongoSettings settings)
        {
            if (string.IsNullOrWhiteSpace(settings.ConnectionString))
                throw new InvalidOperationException("MongoSettings:ConnectionString must be configured.");

            if (string.IsNullOrWhiteSpace(settings.DatabaseName))
                throw new InvalidOperationException("MongoSettings:DatabaseName must be configured.");
        }

        private static IServiceCollection AddRepositories(IServiceCollection services) {
            services.AddScoped(typeof(IBaseWriteRepository<>), typeof(BaseWriteRepository<>));    
            services.AddScoped(typeof(IBaseReadRepository<>), typeof(BaseReadRepository<>));
                
            return services;      
        }
    }
}
