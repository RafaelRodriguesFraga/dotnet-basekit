
using DotnetBaseKit.Components.Application.Pagination;

namespace DotnetBaseKit.Components.Api.Responses
{
    public class Response
    {
        public Response()
        {

        }       

        public Response(bool success)
        {
            Success = success;
        }    

        public Response(bool success, IEnumerable<string> errors) : this(success)
        {
            Errors = errors;
        }            

        public bool Success { get;  set; }
        public IEnumerable<string> Errors { get; set; }

    }

    public class Response<TData> : Response
     where TData : class
    {
        public Response()
        {

        }

        public Response(TData data, bool success)
            : base(success)
        {
            Data = data;
            Success = success;       
        }

        public Response(TData data, bool success, IEnumerable<string> errors)
            : this(data, success)
        {
            Errors = errors;
        }

        public TData Data { get; set; }

    }



    public class ResponseList<TData> : Response
        where TData : class
    {
        public ResponseList()
        {

        }

        public ResponseList(IEnumerable<TData> data)
        {
            Data = data;
        }

        public ResponseList(IEnumerable<TData> data, bool success)
          : base(success)
        {
            Data = data;
        }

        public ResponseList(IEnumerable<TData> data, bool success, IEnumerable<string> errors)
            : this(data, success)
        {
            Errors = errors;
        }

        public IEnumerable<TData> Data { get; set; }
    }

    public class ResponsePaginated<TData> : ResponseList<TData>
       where TData : class
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public long TotalRecords { get; set; }

        public ResponsePaginated()
        {
        }

        public ResponsePaginated(IPaginationResponse<TData> data, bool success)
            : base(data.Data, success)
        {
            SetPageData(data);
        }

        public ResponsePaginated(IPaginationResponse<TData> data, bool success, IEnumerable<string> messages)
            : base(data.Data, success, messages)
        {
            SetPageData(data);
        }

        private void SetPageData(IPaginationResponse<TData> data)
        {
            CurrentPage = (int)data.CurrentPage;
            TotalPages = (int)data.TotalPages;
            TotalRecords = data.TotalRecords;
        }
    }
}
