using DotnetBaseKit.Components.Application.Pagination;

namespace DotnetBaseKit.Components.Tests.ServiceApplication.Pagination;

public class PaginationResponseTests
{
    [Fact(DisplayName = "Should create PaginationResponse with default constructor")]
    public void Should_Create_PaginationResponse_DefaultConstructor()
    {
        var pagination = new PaginationResponse<string>();

        Assert.NotNull(pagination);
        Assert.Null(pagination.Data);
        Assert.Equal(0, pagination.CurrentPage);
        Assert.Equal(0, pagination.QuantityPerPage);
        Assert.Equal(0, pagination.TotalPages);
        Assert.Equal(0, pagination.TotalRecords);
    }

    [Fact(DisplayName = "Should correctly calculate TotalPages and set properties")]
    public void Should_Calculate_TotalPages_And_Set_Properties()
    {
        int currentPage = 1;
        int quantityPerPage = 10;
        long totalRecords = 2;
        
        var data = new List<string> { "Data1", "Data2" };

        var pagination = new PaginationResponse<string>(currentPage, quantityPerPage, totalRecords, data);

        Assert.Equal(currentPage, pagination.CurrentPage);
        Assert.Equal(quantityPerPage, pagination.QuantityPerPage);
        Assert.Equal(totalRecords, pagination.TotalRecords);
        Assert.Equal(data, pagination.Data);
        Assert.Equal(1, pagination.TotalPages);
    }
}