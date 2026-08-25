namespace DotnetBaseKit.Components.Application.Pagination
{
    public class PaginationResponse<TData> : IPaginationResponse<TData> where TData : class
    {
        public PaginationResponse()
        {

        }
        public PaginationResponse(int currentPage, int quantityPerPage, long totalRecords, IEnumerable<TData> data)
        {
            ArgumentOutOfRangeException.ThrowIfLessThan(currentPage, 1);
            ArgumentOutOfRangeException.ThrowIfLessThan(quantityPerPage, 1);
            ArgumentOutOfRangeException.ThrowIfNegative(totalRecords);

            CurrentPage = currentPage;
            QuantityPerPage = quantityPerPage;
            TotalRecords = totalRecords;
            TotalPages = (int)Math.Ceiling((double)totalRecords / (double)quantityPerPage);
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public int CurrentPage { get; set; }
        public int QuantityPerPage { get; set; }
        public long TotalRecords { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<TData> Data { get; set; } = Array.Empty<TData>();
    }
}
