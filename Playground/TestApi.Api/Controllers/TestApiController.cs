using DotnetBoilerplate.Components.Api.Base;
using DotnetBoilerplate.Components.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using TestApi.Application.Services;
using TestApi.Application.ViewModels;

namespace TestApi.Api.Controllers

{
    [Route("api/[controller]")]
    public class TestApiController : ApiControllerBase
    {
        private readonly ITestApiServiceApplication _testApiServiceApplication;
        private readonly ITestApiSqlServiceApplication _testApiSqlServiceApplication;
        public TestApiController(IResponseFactory responseFactory, ITestApiServiceApplication testApiServiceApplication, ITestApiSqlServiceApplication testApiSqlServiceApplication) : base(responseFactory)
        {
            _testApiServiceApplication = testApiServiceApplication;
            _testApiSqlServiceApplication = testApiSqlServiceApplication;
        }

        [HttpPost]
        public async Task<IActionResult> InsertAsync(TestApiViewModel viewModel)
        {
            await _testApiServiceApplication.CreateAsync(viewModel);

            return ResponseCreated();
        }
        [HttpPost("sql")]
        public async Task<IActionResult> InsertSqlAsync(TestApiViewModel viewModel)
        {
            await _testApiSqlServiceApplication.CreateAsync(viewModel);

            return ResponseCreated();
        }

        [HttpPut("sql/update/{id}")]
        public async Task<IActionResult> UpdateSqlAsync([FromRoute] Guid id, TestApiViewModel viewModel)
        {
            await _testApiSqlServiceApplication.UpdateAsync(id, viewModel);

            return CreateResponse();
        }

        [HttpDelete("sql/delete/{id}")]
        public async Task<IActionResult> DeleteSqlAsync([FromRoute] Guid id)
        {
            await _testApiSqlServiceApplication.DeleteAsync(id);

            return CreateResponse();
        }
    }
}
