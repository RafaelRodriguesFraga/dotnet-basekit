using DotnetBoilerplate.Components.Infra.Sql.Context.Base;
using DotnetBoilerplate.Components.Infra.Sql.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Domain.Entities;
using TestApi.Domain.Repositories;

namespace TestApi.Infra
{
    public class TestApiSqlReadRepository : BaseReadRepository<TestSql>, ITestApiSqlReadRepository
    {
        public TestApiSqlReadRepository(BaseContext context) : base(context)
        {
        }
    }
}
