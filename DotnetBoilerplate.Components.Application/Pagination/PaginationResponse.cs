namespace DotnetBoilerplate.Components.Application.Pagination
{
    public class PaginationResponse<TData> : IPaginationResponse<TData> where TData : class
    {
        public PaginationResponse(int currentPage, int totalPages)
        {
            CurrentPage = currentPage;
            TotalPages = totalPages;
        }

        public PaginationResponse(IEnumerable<TData> data, int currentPage, int totalPages, long totalRecords)
        {
            Data = data;
            CurrentPage = currentPage;
            TotalPages = totalPages;
            TotalRecords = totalRecords;
        }

        public IEnumerable<TData> Data {get; set;}
        public int CurrentPage {get; set;}    
        public int TotalPages {get; set;}
        public long TotalRecords {get; set;}      
    }
}
