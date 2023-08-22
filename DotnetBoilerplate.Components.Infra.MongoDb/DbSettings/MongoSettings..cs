namespace DotnetBoilerplate.Components.Infra.MongoDb.DbSettings
{
    public class MongoSettings : IMongoSettings
    {
        public string DatabaseName { get; set; }
        public string ConnectionString { get; set; }
    }
}