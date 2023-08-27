using DotnetBoilerplate.Components.Domain.Sql.Repositories;
using TestApi.Domain.Entities;

namespace TestApi.Domain.Repositories
{
    public interface ITestApiSqlReadRepository : IBaseReadRepository<TestSql>
    {
    }
}
