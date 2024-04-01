using Microsoft.EntityFrameworkCore;

namespace DotnetBaseKit.Components.Infra.Sql.Context.Base
{
    public class BaseContext : DbContext
    {
        protected BaseContext() : base() { }

        protected BaseContext(DbContextOptions options) : base(options) { }
    }
}
