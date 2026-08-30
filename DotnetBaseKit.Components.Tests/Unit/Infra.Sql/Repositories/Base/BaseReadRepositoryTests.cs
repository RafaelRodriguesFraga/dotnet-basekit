using DotnetBaseKit.Components.Infra.Sql.Repositories.Base;
using DotnetBaseKit.Components.Tests.Mocks;
using DotnetBaseKit.Components.Tests.Mocks.Utils;

namespace DotnetBaseKit.Components.Tests.Unit.Infra.Sql.Repositories.Base;

public class BaseReadRepositoryTests
{
    [Fact(DisplayName = "Should get entity by id")]
    public void Should_Get_Entity_By_Id()
    {
        using var context = DbContextUtils.CreateContext();

        var entity = new FakeBaseEntitySqlWithData("Test");

        context.Set<FakeBaseEntitySqlWithData>().Add(entity);
        context.SaveChanges();

        var repository =
            new BaseReadRepository<FakeBaseEntitySqlWithData>(context);

        var result = repository.GetById(entity.Id);

        Assert.NotNull(result);
        Assert.Equal(entity.Id, result.Id);
        Assert.Equal("Test", result.Name);
    }

    [Fact(DisplayName = "Should return null when entity does not exist")]
    public void Should_Return_Null_When_Entity_Does_Not_Exist()
    {
        using var context = DbContextUtils.CreateContext();

        var repository =
            new BaseReadRepository<FakeBaseEntitySqlWithData>(context);

        var result = repository.GetById(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact(DisplayName = "Should get entity by id asynchronously")]
    public async Task Should_Get_Entity_By_Id_Asynchronously()
    {
        await using var context = DbContextUtils.CreateContext();

        var entity = new FakeBaseEntitySqlWithData("Test");

        context.Set<FakeBaseEntitySqlWithData>().Add(entity);
        await context.SaveChangesAsync();

        var repository =
            new BaseReadRepository<FakeBaseEntitySqlWithData>(context);

        var result = await repository.GetByIdAsync(entity.Id);

        Assert.NotNull(result);
        Assert.Equal(entity.Id, result.Id);
        Assert.Equal("Test", result.Name);
    }

    [Fact(DisplayName = "Should return null when entity does not exist asynchronously")]
    public async Task Should_Return_Null_When_Entity_Does_Not_Exist_Asynchronously()
    {
        await using var context = DbContextUtils.CreateContext();

        var repository =
            new BaseReadRepository<FakeBaseEntitySqlWithData>(context);

        var result = await repository.GetByIdAsync(Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact(DisplayName = "Should get all entities")]
    public void Should_Get_All_Entities()
    {
        using var context = DbContextUtils.CreateContext();

        var entities = new[]
        {
            new FakeBaseEntitySql(),
            new FakeBaseEntitySql(),
            new FakeBaseEntitySql()
        };

        context.Set<FakeBaseEntitySql>().AddRange(entities);
        context.SaveChanges();

        var repository =
            new BaseReadRepository<FakeBaseEntitySql>(context);

        var result = repository.GetAll();

        Assert.Equal(3, result.Count());
    }

    [Fact(DisplayName = "Should return empty collection when there are no entities")]
    public void Should_Return_Empty_Collection_When_There_Are_No_Entities()
    {
        using var context = DbContextUtils.CreateContext();

        var repository =
            new BaseReadRepository<FakeBaseEntitySql>(context);

        var result = repository.GetAll();

        Assert.Empty(result);
    }

    [Fact(DisplayName = "Should get all entities asynchronously")]
    public async Task Should_Get_All_Entities_Asynchronously()
    {
        await using var context = DbContextUtils.CreateContext();

        var entities = new[]
        {
            new FakeBaseEntitySql(),
            new FakeBaseEntitySql(),
            new FakeBaseEntitySql()
        };

        context.Set<FakeBaseEntitySql>().AddRange(entities);
        await context.SaveChangesAsync();

        var repository =
            new BaseReadRepository<FakeBaseEntitySql>(context);

        var result = await repository.GetAllAsync();

        Assert.Equal(3, result.Count());
    }

    [Fact(DisplayName = "Should return empty collection when there are no entities asynchronously")]
    public async Task Should_Return_Empty_Collection_When_There_Are_No_Entities_Asynchronously()
    {
        await using var context = DbContextUtils.CreateContext();

        var repository =
            new BaseReadRepository<FakeBaseEntitySql>(context);

        var result = await repository.GetAllAsync();

        Assert.Empty(result);
    }

    [Fact(DisplayName = "Should get paginated entities")]
    public async Task Should_Get_Paginated_Entities()
    {
        await using var context = DbContextUtils.CreateContext();

        var entities = CreateEntities(5);

        context.Set<FakeBaseEntitySqlWithData>().AddRange(entities);
        await context.SaveChangesAsync();

        var repository =
            new BaseReadRepository<FakeBaseEntitySqlWithData>(context);

        var (result, totalRecords) =
            await repository.GetAllPaginatedAsync(1, 2);

        Assert.Equal(5, totalRecords);
        Assert.Equal(2, result.Count());
    }

    [Fact(DisplayName = "Should skip previous pages when getting paginated entities")]
    public async Task Should_Skip_Previous_Pages_When_Getting_Paginated_Entities()
    {
        await using var context = DbContextUtils.CreateContext();

        var entities = CreateEntities(5);

        context.Set<FakeBaseEntitySqlWithData>().AddRange(entities);
        await context.SaveChangesAsync();

        var repository =
            new BaseReadRepository<FakeBaseEntitySqlWithData>(context);

        var (result, totalRecords) =
            await repository.GetAllPaginatedAsync(2, 2);

        Assert.Equal(5, totalRecords);
        Assert.Equal(2, result.Count());
    }

    [Fact(DisplayName = "Should return remaining entities on last page")]
    public async Task Should_Return_Remaining_Entities_On_Last_Page()
    {
        await using var context = DbContextUtils.CreateContext();

        var entities = CreateEntities(5);

        context.Set<FakeBaseEntitySqlWithData>().AddRange(entities);
        await context.SaveChangesAsync();

        var repository =
            new BaseReadRepository<FakeBaseEntitySqlWithData>(context);

        var (result, totalRecords) =
            await repository.GetAllPaginatedAsync(3, 2);

        Assert.Equal(5, totalRecords);
        Assert.Single(result);
    }

    [Fact(DisplayName = "Should return all records when quantity per page is greater than total records")]
    public async Task Should_Return_All_Records_When_Quantity_Per_Page_Is_Greater_Than_Total_Records()
    {
        await using var context = DbContextUtils.CreateContext();

        var entities = CreateEntities(5);

        context.Set<FakeBaseEntitySqlWithData>().AddRange(entities);
        await context.SaveChangesAsync();

        var repository =
            new BaseReadRepository<FakeBaseEntitySqlWithData>(context);

        var (result, totalRecords) =
            await repository.GetAllPaginatedAsync(1, 10);

        Assert.Equal(5, totalRecords);
        Assert.Equal(5, result.Count());
    }

    [Fact(DisplayName = "Should return empty collection when page is beyond available pages")]
    public async Task Should_Return_Empty_Collection_When_Page_Is_Beyond_Available_Pages()
    {
        await using var context = DbContextUtils.CreateContext();

        var entities = CreateEntities(5);

        context.Set<FakeBaseEntitySqlWithData>().AddRange(entities);
        await context.SaveChangesAsync();

        var repository =
            new BaseReadRepository<FakeBaseEntitySqlWithData>(context);

        var (result, totalRecords) =
            await repository.GetAllPaginatedAsync(4, 2);

        Assert.Equal(5, totalRecords);
        Assert.Empty(result);
    }

    [Fact(DisplayName = "Should order paginated entities by created date descending")]
    public async Task Should_Order_Paginated_Entities_By_Created_Date_Descending()
    {
        await using var context = DbContextUtils.CreateContext();

        var oldest = new FakeBaseEntitySqlWithData("Oldest");

        await Task.Delay(10);

        var middle = new FakeBaseEntitySqlWithData("Middle");

        await Task.Delay(10);

        var newest = new FakeBaseEntitySqlWithData("Newest");

        context.Set<FakeBaseEntitySqlWithData>().AddRange(
            oldest,
            middle,
            newest);

        await context.SaveChangesAsync();

        var repository =
            new BaseReadRepository<FakeBaseEntitySqlWithData>(context);

        var (result, _) =
            await repository.GetAllPaginatedAsync(1, 10);

        var list = result.ToList();

        Assert.Equal(3, list.Count);
        Assert.Equal("Newest", list[0].Name);
        Assert.Equal("Middle", list[1].Name);
        Assert.Equal("Oldest", list[2].Name);
    }

    private static List<FakeBaseEntitySqlWithData> CreateEntities(int quantity)
    {
        return Enumerable
            .Range(1, quantity)
            .Select(i => new FakeBaseEntitySqlWithData($"Entity {i}"))
            .ToList();
    }


}