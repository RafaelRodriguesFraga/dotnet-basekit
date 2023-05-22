using Fraga.Components.Api.Base;
using Fraga.Components.Api.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using TestApi.Application.Services;
using TestApi.Application.ViewModels;

namespace TestApi.Api.Controllers

{
    [Route("api/[controller]")]
    public class TestApiController : ApiControllerBase
    {
        private readonly ITestApiServiceApplication _testApiServiceApplication;
        public TestApiController(IResponseFactory responseFactory, ITestApiServiceApplication testApiServiceApplication) : base(responseFactory)
        {
            _testApiServiceApplication = testApiServiceApplication;
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync(TestApiViewModel viewModel)
        {
            await _testApiServiceApplication.CreateAsync(viewModel);

            return ResponseCreated();
        }
    }
}
