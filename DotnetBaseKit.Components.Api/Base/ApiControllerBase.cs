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
        protected IActionResult CreateResponse()
        {
            var response = _responseFactory.Create();

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
        protected IActionResult ResponseOk<TData>(TData result) where TData : class
        {
            var response = _responseFactory.Create(result);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        protected IActionResult ResponseOk<TData>(IEnumerable<TData> result) where TData : class
        {
            var response = _responseFactory.Create(result);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        protected IActionResult ResponseOk<TData>(PaginationResponse<TData> searchResult) where TData : class
        {
            var response = _responseFactory.Create(searchResult);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        protected IActionResult ResponseCreated<TData>(TData result) where TData : class
        {
            var response = _responseFactory.Create(result);

            if (!response.Success)
                return BadRequest(response);

            return Created("", response);
        }

        protected IActionResult ResponseCreated()
        {
            var response = _responseFactory.Create();

            if (!response.Success)
                return BadRequest(response);

            return Created("", response);

        }

        protected IActionResult ResponseBadRequest<TData>(TData result) where TData : class
        {
            var response = _responseFactory.Create(result);

            if (!response.Success)
                return BadRequest(response);

            return null;

        }
    }
}
