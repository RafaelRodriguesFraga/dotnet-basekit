using DotnetBaseKit.Components.Application.Pagination;

namespace DotnetBaseKit.Components.Api.Responses
{
    public interface IResponseFactory
    {
        Response Create();
        Response<TData> Create<TData>(TData data) where TData : class;
        ResponseList<TData> Create<TData>(IEnumerable<TData> dataList)
          where TData : class;
        ResponsePaginated<TData> Create<TData>(PaginationResponse<TData> searchResult)
            where TData : class;

    }
}
