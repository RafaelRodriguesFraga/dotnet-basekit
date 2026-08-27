using DotnetBaseKit.Components.Infra.Sql.Context.Base;
using DotnetBaseKit.Components.Infra.Sql.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Domain.Entities;
using TestApi.Domain.Repositories;

namespace TestApi.Infra
{
    public class TestApiSqlWriteRepository : BaseWriteRepository<TestSql>, ITestApiSqlWriteRepository
    {
        public TestApiSqlWriteRepository(BaseContext context) : base(context)
        {
        }
    }
}
