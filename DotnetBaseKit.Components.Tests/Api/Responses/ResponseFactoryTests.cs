using DotnetBaseKit.Components.Shared.Notifications;
using DotnetBaseKit.Components.Api.Responses;
using DotnetBaseKit.Components.Application.Pagination;
using Moq;

namespace DotnetBaseKit.Tests.Api.Responses
{
    public class ResponseFactoryTests
    {
        private readonly Mock<INotificationMessageFormatter> _formatterMock;
        private readonly NotificationContext _notificationContext;

        public ResponseFactoryTests()
        {
            _formatterMock = new Mock<INotificationMessageFormatter>();
            _notificationContext = new NotificationContext();
        }

        private ResponseFactory CreateFactory(bool includeKey = true)
        {
            return new ResponseFactory(_notificationContext, _formatterMock.Object, includeKey);
        }

        [Fact(DisplayName = "Should return success response without data")]
        public void Should_Return_Success_Response_Without_Data()
        {
            _formatterMock.Setup(f => f.Format(It.IsAny<IReadOnlyCollection<Notification>>(), true))
                          .Returns(new List<string>());

            var factory = CreateFactory();
            var response = factory.Create();

            Assert.True(response.Success);
            Assert.Empty(response.Errors);
        }

        [Fact(DisplayName = "Should return failure response with error messages")]
        public void Should_Return_Failure_Response_With_Errors()
        {
            _notificationContext.AddNotification("Invalid input");
            _notificationContext.AddNotification("Missing field");

            var expectedErrors = new List<string> { "Invalid input", "Missing field" };

            _formatterMock.Setup(f => f.Format(It.IsAny<IReadOnlyCollection<Notification>>(), false))
                          .Returns(expectedErrors);

            var factory = CreateFactory(includeKey: false);
            var response = factory.Create();

            Assert.False(response.Success);
            Assert.Equal(expectedErrors, response.Errors);
        }

        [Fact(DisplayName = "Should return success response with data")]
        public void Should_Return_Success_Response_With_Data()
        {
            var data = new DummyDto { Id = 1, Name = "Test" };

            _formatterMock.Setup(f => f.Format(It.IsAny<IReadOnlyCollection<Notification>>(), true))
                          .Returns(new List<string>());

            var factory = CreateFactory();
            var response = factory.Create(data);

            Assert.True(response.Success);
            Assert.Equal(data, response.Data);
            Assert.Empty(response.Errors);
        }

        [Fact(DisplayName = "Should return success response with list of data")]
        public void Should_Return_Success_Response_With_List_Of_Data()
        {
            var data = new List<DummyDto>
            {
                new DummyDto { Id = 1, Name = "Item 1" },
                new DummyDto { Id = 2, Name = "Item 2" }
            };

            _formatterMock.Setup(f => f.Format(It.IsAny<IReadOnlyCollection<Notification>>(), true))
                          .Returns(new List<string>());

            var factory = CreateFactory();
            var response = factory.Create(data.AsEnumerable());

            Assert.True(response.Success);
            Assert.Equal(data, response.Data);
            Assert.Empty(response.Errors);
        }

        [Fact(DisplayName = "Should return paginated response with pagination info")]
        public void Should_Return_Paginated_Response_With_Pagination_Info()
        {
            var data = new List<DummyDto>
            {
                new DummyDto { Id = 1, Name = "Paginated Item 1" }
            };

            var pagination = new PaginationResponse<DummyDto>
            {
                Data = data,
                CurrentPage = 1,
                TotalPages = 3,
                TotalRecords = 20
            };

            _formatterMock.Setup(f => f.Format(It.IsAny<IReadOnlyCollection<Notification>>(), true))
                          .Returns(new List<string>());

            var factory = CreateFactory();
            var response = factory.Create(pagination);

            Assert.True(response.Success);
            Assert.Equal(data, response.Data);
            Assert.Equal(1, response.CurrentPage);
            Assert.Equal(3, response.TotalPages);
            Assert.Equal(20, response.TotalRecords);
        }
    }

    public class DummyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}