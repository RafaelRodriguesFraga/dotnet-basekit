namespace DotnetBaseKit.Components.Infra.MongoDb.DbSettings
{
    public interface IMongoSettings
    {
        public string DatabaseName { get; set; }
       public string ConnectionString { get; set; }
    }
}