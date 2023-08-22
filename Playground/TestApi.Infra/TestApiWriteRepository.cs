using DotnetBoilerplate.Components.Infra.MongoDb.DbSettings;
using DotnetBoilerplate.Components.Infra.MongoDb.Repositories.Base;
using TestApi.Domain.Entities;
using TestApi.Domain.Repositories;

namespace TestApi.Infra
{
    public class TestApiWriteRepository : BaseWriteRepository<Test>, ITestApiWriteRepository
    {
        public TestApiWriteRepository(IMongoSettings settings) : base(settings)
        {
        }
    }
}
