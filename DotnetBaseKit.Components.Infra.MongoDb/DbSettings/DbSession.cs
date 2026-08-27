using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DotnetBaseKit.Components.Infra.MongoDb.DbSettings
{
    public class DbSession
    {

        public DbSession(IMongoClient client, IMongoSettings settings)
        {
            Client = client ?? throw new ArgumentNullException(nameof(client));
            ArgumentNullException.ThrowIfNull(settings);

            if (string.IsNullOrWhiteSpace(settings.DatabaseName))
                throw new InvalidOperationException("MongoSettings:DatabaseName must be configured.");

            Database = Client.GetDatabase(settings.DatabaseName);
        }

        public IMongoClient Client { get; }
        public IMongoDatabase Database { get; }
    }
}
