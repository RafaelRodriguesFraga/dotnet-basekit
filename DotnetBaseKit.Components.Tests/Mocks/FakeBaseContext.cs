using DotnetBaseKit.Components.Infra.Sql.Context.Base;
using DotnetBaseKit.Components.Shared.Notifications;
using Microsoft.EntityFrameworkCore;

namespace DotnetBaseKit.Components.Tests.Mocks;

public class FakeBaseContext : BaseContext
{
    public FakeBaseContext(DbContextOptions<FakeBaseContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Ignore<Notification>();
        modelBuilder.Entity<FakeBaseEntitySql>();
        modelBuilder.Entity<FakeBaseEntitySqlWithData>();
    }


}