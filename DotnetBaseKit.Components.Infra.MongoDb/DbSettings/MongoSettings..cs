namespace DotnetBaseKit.Components.Infra.MongoDb.DbSettings
{
    public class MongoSettings : IMongoSettings
    {
        public string DatabaseName { get; set; } = string.Empty;
        public string ConnectionString { get; set; } = string.Empty;
    }
}
