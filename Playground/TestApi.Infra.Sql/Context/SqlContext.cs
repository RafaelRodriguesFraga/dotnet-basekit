using DotnetBaseKit.Components.Infra.Sql.Context.Base;
using DotnetBaseKit.Components.Shared.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApi.Infra.Sql.Configurations;

namespace TestApi.Infra.Sql.Context
{
    public class SqlContext : BaseContext
    {
        public SqlContext(DbContextOptions<SqlContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
             modelBuilder.Ignore<Notification>();
            modelBuilder.ApplyConfiguration(new TestSqlConfiguration());
        }
    }
}
