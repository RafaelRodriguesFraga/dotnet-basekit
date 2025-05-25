using DotnetBaseKit.Components.Domain.MongoDb.Repositories.Base;
using DotnetBaseKit.Components.Infra.MongoDb.DbSettings;
using DotnetBaseKit.Components.Infra.MongoDb.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MongoExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
            services.Configure<MongoSettings>(configuration.GetSection("MongoSettings"));
            services.AddSingleton<IMongoSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoSettings>>().Value);
           
            AddRepositories(services);
            return services;
        }

        private static IServiceCollection AddRepositories(IServiceCollection services) {
            services.AddScoped(typeof(IBaseWriteRepository<>), typeof(BaseWriteRepository<>));    

            return services;      
        }
    }
}