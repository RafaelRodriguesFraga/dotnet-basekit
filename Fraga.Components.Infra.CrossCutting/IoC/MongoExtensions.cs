using Fraga.Components.Domain.MongoDb.Repositories.Base;
using Fraga.Components.Infra.MongoDb.DbSettings;
using Fraga.Components.Infra.MongoDb.Repositories.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace Fraga.Components.Infra.CrossCutting.IoC
{
    public static class MongoExtensions
    {
        public static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
        {
            BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;
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