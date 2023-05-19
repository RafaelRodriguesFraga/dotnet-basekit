namespace Fraga.Components.Application.Pagination
{
    public interface IPaginationResponse<TData> where TData : class
    {
        IEnumerable<TData> Data { get; set; }
        int CurrentPage { get; set; }
        int TotalPages { get; set; }
        long TotalRecords { get; set; }  


    }
}