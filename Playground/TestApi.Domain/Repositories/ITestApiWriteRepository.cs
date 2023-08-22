using DotnetBoilerplate.Components.Domain.MongoDb.Repositories.Base;
using TestApi.Domain.Entities;

namespace TestApi.Domain.Repositories
{
    public interface ITestApiWriteRepository : IBaseWriteRepository<Test>
    {
    }
}
