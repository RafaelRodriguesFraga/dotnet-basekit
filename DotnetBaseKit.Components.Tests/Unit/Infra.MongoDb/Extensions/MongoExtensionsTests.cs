using DotnetBaseKit.Components.Domain.MongoDb.Repositories.Base;
using DotnetBaseKit.Components.Infra.MongoDb;
using DotnetBaseKit.Components.Infra.MongoDb.DbSettings;
using DotnetBaseKit.Components.Tests.Mocks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetBaseKit.Components.Tests.Unit.Infra.MongoDb.Extensions
{
    public class MongoExtensionsTests
    {
        [Fact(DisplayName = "AddMongoDb should register MongoSettings and repositories correctly")]
        public void AddMongoDb_Should_Register_MongoSettings_And_Repositories_Correctly()
        {
            var services = new ServiceCollection();

            var inMemorySettings = new Dictionary<string, string>
            {
                ["MongoSettings:ConnectionString"] = "mongodb://localhost:27017",
                ["MongoSettings:DatabaseName"] = "TestDb"
            };

            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings!)
                .Build();

            services.AddMongoDb(configuration);

            var provider = services.BuildServiceProvider();

            var mongoSettings = provider.GetService<IMongoSettings>();
            Assert.NotNull(mongoSettings);
            Assert.Equal("mongodb://localhost:27017", mongoSettings.ConnectionString);
            Assert.Equal("TestDb", mongoSettings.DatabaseName);

            var writeRepository = provider.GetService(typeof(IBaseWriteRepository<FakeBaseEntityMongo>));
            Assert.NotNull(writeRepository);

            var readRepository = provider.GetService(typeof(IBaseReadRepository<FakeBaseEntityMongo>));
            Assert.NotNull(readRepository);
        }
    }
}