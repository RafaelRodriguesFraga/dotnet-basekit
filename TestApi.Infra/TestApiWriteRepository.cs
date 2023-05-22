using Fraga.Components.Infra.MongoDb.DbSettings;
using Fraga.Components.Infra.MongoDb.Repositories.Base;
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
