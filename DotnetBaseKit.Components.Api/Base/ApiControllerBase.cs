using DotnetBaseKit.Components.Api.Responses;
using DotnetBaseKit.Components.Application.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace DotnetBaseKit.Components.Api.Base
{
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly IResponseFactory _responseFactory;

        protected ApiControllerBase(IResponseFactory responseFactory)
        {
            _responseFactory = responseFactory;
        }


        private IActionResult BuildResponse(Response response, Func<object, IActionResult> successResult)
        {
            if (!response.Success)
                return BadRequest(response);

            return successResult(response);
        }

        protected IActionResult CreateResponse()
        {
            var response = _responseFactory.Create();

            return BuildResponse(response, Ok);
        }

        protected IActionResult ResponseOk<TData>(TData result) where TData : class
        {
            var response = _responseFactory.Create(result);

            return BuildResponse(response, Ok);
        }

        protected IActionResult ResponseOk<TData>(IEnumerable<TData> result) where TData : class
        {
            var response = _responseFactory.Create(result);
            return BuildResponse(response, Ok);
        }

        protected IActionResult ResponseOk<TData>(PaginationResponse<TData> searchResult) where TData : class
        {
            var response = _responseFactory.Create(searchResult);
            return BuildResponse(response, Ok);
        }

        protected IActionResult ResponseCreated<TData>(TData result) where TData : class
        {
            var response = _responseFactory.Create(result);

            return BuildResponse(response, r => Created(string.Empty, r));
        }

        protected IActionResult ResponseCreated()
        {
            var response = _responseFactory.Create();
            return BuildResponse(response, r => Created(string.Empty, r));
        }

            // BadRequest genérico (ainda que o retorno null não seja muito usual, pode ser ajustado)
        protected IActionResult ResponseBadRequest<TData>(TData result) where TData : class
        {
            var response = _responseFactory.Create(result);

            if (!response.Success)
                return BadRequest(response);

            return null;
        }

        protected IActionResult ResponseConflict<TData>(TData result) where TData : class
        {
            var response = _responseFactory.Create(result);
            return Conflict(response);
        }

        protected IActionResult ResponseUnprocessableEntity<TData>(TData result) where TData : class
        {
            var response = _responseFactory.Create(result);

            return UnprocessableEntity(response);
        }

    }
}
