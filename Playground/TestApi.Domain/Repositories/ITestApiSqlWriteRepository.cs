using DotnetBaseKit.Components.Domain.Sql.Repositories;
using TestApi.Domain.Entities;

namespace TestApi.Domain.Repositories
{
    public interface ITestApiSqlWriteRepository : IBaseWriteRepository<TestSql>
    {
    }
}
