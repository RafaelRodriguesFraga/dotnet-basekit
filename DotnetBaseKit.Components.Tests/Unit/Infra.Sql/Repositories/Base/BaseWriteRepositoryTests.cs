using DotnetBaseKit.Components.Infra.Sql.Context.Base;
using DotnetBaseKit.Components.Infra.Sql.Repositories.Base;
using DotnetBaseKit.Components.Tests.Mocks;
using Microsoft.EntityFrameworkCore;

namespace DotnetBaseKit.Components.Tests.Unit.Infra.Sql.Repositories.Base;

public class BaseWriteRepositoryTests
{
    [Fact(DisplayName = "Should insert entity")]
    public void Should_Insert_Entity()
    {
        using var context = CreateContext();

        var repository =
            new BaseWriteRepository<FakeBaseEntitySqlWithData>(context);

        var entity = new FakeBaseEntitySqlWithData("Test");

        repository.Insert(entity);

        var result = context
            .Set<FakeBaseEntitySqlWithData>()
            .Find(entity.Id);

        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
    }

    [Fact(DisplayName = "Should insert entity asynchronously")]
    public async Task Should_Insert_Entity_Asynchronously()
    {
        await using var context = CreateContext();

        var repository =
            new BaseWriteRepository<FakeBaseEntitySqlWithData>(context);

        var entity = new FakeBaseEntitySqlWithData("Test");

        await repository.InsertAsync(entity);

        var result = await context
            .Set<FakeBaseEntitySqlWithData>()
            .FindAsync(entity.Id);

        Assert.NotNull(result);
        Assert.Equal("Test", result.Name);
    }

    [Fact(DisplayName = "Should update entity")]
    public void Should_Update_Entity()
    {
        using var context = CreateContext();

        var entity = new FakeBaseEntitySqlWithData("Test");

        context.Set<FakeBaseEntitySqlWithData>().Add(entity);
        context.SaveChanges();

        var repository =
            new BaseWriteRepository<FakeBaseEntitySqlWithData>(context);

        entity.ChangeName("New name");

        repository.Update(entity);

        var result = context
            .Set<FakeBaseEntitySqlWithData>()
            .Find(entity.Id);

        Assert.NotNull(result);
        Assert.Equal("New name", result.Name);
    }

    [Fact(DisplayName = "Should update entity asynchronously")]
    public async Task Should_Update_Entity_Asynchronously()
    {
        await using var context = CreateContext();

        var entity = new FakeBaseEntitySqlWithData("Test");

        context.Set<FakeBaseEntitySqlWithData>().Add(entity);
        await context.SaveChangesAsync();

        var repository =
            new BaseWriteRepository<FakeBaseEntitySqlWithData>(context);

        entity.ChangeName("New name");

        await repository.UpdateAsync(entity);

        var result = await context
            .Set<FakeBaseEntitySqlWithData>()
            .FindAsync(entity.Id);

        Assert.NotNull(result);
        Assert.Equal("New name", result.Name);
    }

    [Fact(DisplayName = "Should mark entity for deletion")]
    public void Should_Mark_Entity_For_Deletion()
    {
        using var context = CreateContext();

        var entity = new FakeBaseEntitySqlWithData("Test");

        context.Set<FakeBaseEntitySqlWithData>().Add(entity);
        context.SaveChanges();

        var repository =
            new BaseWriteRepository<FakeBaseEntitySqlWithData>(context);

        repository.Delete(entity);

        Assert.Equal(
            EntityState.Deleted,
            context.Entry(entity).State);
    }

    [Fact(DisplayName = "Should delete entity asynchronously")]
    public async Task Should_Delete_Entity_Asynchronously()
    {
        await using var context = CreateContext();

        var entity = new FakeBaseEntitySqlWithData("Test");

        context.Set<FakeBaseEntitySqlWithData>().Add(entity);
        await context.SaveChangesAsync();

        var repository =
            new BaseWriteRepository<FakeBaseEntitySqlWithData>(context);

        await repository.DeleteAsync(entity);

        var result = await context
            .Set<FakeBaseEntitySqlWithData>()
            .FindAsync(entity.Id);

        Assert.Null(result);
    }

    private static FakeBaseContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<FakeBaseContext>()
            .UseSqlite("Data Source=:memory:")
            .Options;

        var context = new FakeBaseContext(options);

        context.Database.OpenConnection();
        context.Database.EnsureCreated();

        return context;
    }
}