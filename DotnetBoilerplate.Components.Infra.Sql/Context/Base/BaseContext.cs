using Microsoft.EntityFrameworkCore;

namespace DotnetBoilerplate.Components.Infra.Sql.Context.Base
{
    public class BaseContext : DbContext
    {
        protected BaseContext() : base() { }

        protected BaseContext(DbContextOptions options) : base(options) { }
    }
}
