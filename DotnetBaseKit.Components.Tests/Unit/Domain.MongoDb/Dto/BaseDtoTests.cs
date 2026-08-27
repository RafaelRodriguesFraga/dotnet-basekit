using DotnetBaseKit.Components.Domain.MongoDb;
using DotnetBaseKit.Components.Tests.Mocks;
using FluentValidation.Results;

namespace DotnetBaseKit.Components.Tests.Unit.Domain.MongoDb.Dto
{
    public class BaseDtoTests
    {
        [Fact(DisplayName = "Should add notifications from ValidationResult")]
        public void Should_Add_Notifications_From_ValidationResult()
        {
            var dto = new FakeBaseDto();
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Field1", "Field1 is required"),
                new ValidationFailure("Field2", "Field2 is invalid")
            });

            dto.AddNotifications(validationResult);

            Assert.False(dto.Valid);
            Assert.Equal(2, dto.Notifications.Count);
            Assert.Contains(dto.Notifications, n => n.Key == "Field1" && n.Message == "Field1 is required");
            Assert.Contains(dto.Notifications, n => n.Key == "Field2" && n.Message == "Field2 is invalid");
        }
    }
}
