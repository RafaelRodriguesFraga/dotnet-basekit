using Moq;
using Microsoft.AspNetCore.Mvc;
using DotnetBaseKit.Components.Api.Base;
using DotnetBaseKit.Components.Api.Responses;
using DotnetBaseKit.Components.Application.Pagination;

namespace DotnetBaseKit.Tests.Api.Controller
{
    public class ApiControllerBaseTests
    {
        private class TestController : ApiControllerBase
        {
            public TestController(IResponseFactory responseFactory) : base(responseFactory) { }

            public IActionResult CallResponseCreated<TData>(TData data) where TData : class => ResponseCreated(data);
            public IActionResult CallCreateResponse() => CreateResponse();
            public IActionResult CallResponseOk<TData>(TData data) where TData : class => ResponseOk(data);
            public IActionResult CallResponseOkList<TData>(IEnumerable<TData> data) where TData : class => ResponseOk(data);
            public IActionResult CallResponsePaginated<TData>(PaginationResponse<TData> data) where TData : class => ResponseOk(data);
            public IActionResult CallResponseCreatedEmpty() => ResponseCreated();
            public IActionResult CallResponseBadRequest<TData>(TData data) where TData : class => ResponseBadRequest(data);
            public IActionResult CallResponseUnprocessableEntity<TData>(TData data) where TData : class => base.ResponseUnprocessableEntity(data);
            public IActionResult CallResponseConflict<TData>(TData data) where TData : class => base.ResponseConflict(data);

        }

        [Fact(DisplayName = "Should return Ok when CreateResponse is successful")]
        public void Should_Return_Ok_When_CreateResponse_Is_Successful()
        {
            var mockFactory = new Mock<IResponseFactory>();
            mockFactory.Setup(f => f.Create())
                .Returns(new Response(true, new List<string>()));

            var controller = new TestController(mockFactory.Object);

            var result = controller.CallCreateResponse();

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Should return Ok when ResponseOk with data is successful")]
        public void Should_Return_Ok_When_ResponseOk_With_Data_Is_Successful()
        {
            var data = new DummyDto { Id = 1, Name = "Item" };
            var mockFactory = new Mock<IResponseFactory>();
            mockFactory.Setup(f => f.Create(data))
                .Returns(new Response<DummyDto>(data, false, new List<string> { "Error" }));

            var controller = new TestController(mockFactory.Object);

            var result = controller.CallResponseOk(data);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Should return Ok when ResponseOk with list is successful")]
        public void Should_Return_Ok_When_ResponseOk_With_List_Is_Successful()
        {

            var responseList = new ResponseList<DummyDto>()
            {
                Data = new List<DummyDto> { new DummyDto { Id = 1, Name = "Item 1" } },
                Errors = new List<string>()
            };


            var mockFactory = new Mock<IResponseFactory>();
            mockFactory.Setup(f => f.Create(responseList.Data))
                .Returns(new ResponseList<DummyDto>(responseList.Data, true, new List<string>()));

            var controller = new TestController(mockFactory.Object);

            var result = controller.CallResponseOkList(responseList.Data);

            Assert.IsType<OkObjectResult>(result);
        }

        [Fact(DisplayName = "Should return Ok when ResponseOk with pagination is successful")]
        public void Should_Return_Ok_When_ResponseOk_With_Pagination_Is_Successful()
        {
            var data = new PaginationResponse<DummyDto>
            {
                Data = new List<DummyDto> { new DummyDto { Id = 1, Name = "Paginated Item" } },
                CurrentPage = 1,
                TotalPages = 1,
                TotalRecords = 1
            };

            var mockFactory = new Mock<IResponseFactory>();
            mockFactory.Setup(f => f.Create(data))
                .Returns(new ResponsePaginated<DummyDto>(data, true, new List<string>()));

            var controller = new TestController(mockFactory.Object);

            var result = controller.CallResponsePaginated(data);

            Assert.IsType<OkObjectResult>(result);
        }


        [Fact(DisplayName = "Should return BadRequest when CreateResponse fails")]
        public void Should_Return_BadRequest_When_CreateResponse_Fails()
        {
            var mockFactory = new Mock<IResponseFactory>();
            mockFactory.Setup(f => f.Create())
                .Returns(new Response(false, new List<string> { "Error" }));

            var controller = new TestController(mockFactory.Object);

            var result = controller.CallCreateResponse();

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Should return Created when ResponseCreated with data is successful")]
        public void Should_Return_Created_When_ResponseCreated_With_Data_Is_Successful()
        {
            var data = new DummyDto { Id = 1, Name = "Item" };

            var mockFactory = new Mock<IResponseFactory>();

            mockFactory.Setup(f => f.Create(data))
                .Returns(new Response<DummyDto>(data, true, new List<string>()));

            var controller = new TestController(mockFactory.Object);

            var result = controller.CallResponseCreated(data);

            Assert.IsType<CreatedResult>(result);
        }

        [Fact(DisplayName = "Should return BadRequest when ResponseCreated with data fails")]
        public void Should_Return_BadRequest_When_ResponseCreated_With_Data_Fails()
        {
            var data = new DummyDto { Id = 1, Name = "Item" };
            var mockFactory = new Mock<IResponseFactory>();
            mockFactory.Setup(f => f.Create(data))
                .Returns(new Response<DummyDto>(data, false, new List<string> { "Error" }));

            var controller = new TestController(mockFactory.Object);

            var result = controller.CallResponseCreated(data);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact(DisplayName = "Should return UnprocessableEntity when ResponseUnprocessableEntity is called")]
        public void Should_Return_UnprocessableEntity_When_ResponseUnprocessableEntity_Is_Called()
        {
            var data = new DummyDto { Id = 1, Name = "Invalid" };
            var mockFactory = new Mock<IResponseFactory>();
            mockFactory.Setup(f => f.Create(data))
                .Returns(new Response<DummyDto>(data, false, new List<string> { "Validation failed" }));

            var controller = new TestController(mockFactory.Object);

            var result = controller.CallResponseUnprocessableEntity(data);

            Assert.IsType<UnprocessableEntityObjectResult>(result);
        }

        [Fact(DisplayName = "Should return Conflict when ResponseConflict is called")]
        public void Should_Return_Conflict_When_ResponseConflict_Is_Called()
        {
            var data = new DummyDto { Id = 1, Name = "Duplicate" };
            var mockFactory = new Mock<IResponseFactory>();
            mockFactory.Setup(f => f.Create(data))
                .Returns(new Response<DummyDto>(data, false, new List<string> { "Already exists" }));

            var controller = new TestController(mockFactory.Object);

            var result = controller.CallResponseConflict(data);

            Assert.IsType<ConflictObjectResult>(result);
        }
    }

    public class DummyDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
