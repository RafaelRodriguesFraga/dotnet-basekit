using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DotnetBaseKit.Components.Infra.MongoDb.DbSettings
{
    public class DbSession
    {

        private readonly MongoClient _client;
        public IMongoDatabase Database { get; private set; }   
        public IConfiguration Configuration { get; private set; }          
    }
}
