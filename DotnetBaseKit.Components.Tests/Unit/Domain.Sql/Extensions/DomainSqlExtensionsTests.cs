using DotnetBaseKit.Components.Domain.Sql;
using DotnetBaseKit.Components.Tests.Mocks;
using FluentValidation.Results;

namespace DotnetBaseKit.Components.Tests.Unit.Domain.Sql.Extensions
{
    public class DomainSqlExtensionsTests
    {
        [Fact(DisplayName = "AddNotifications adds notifications when validation result is invalid")]
        public void AddNotifications_Should_AddNotifications_When_Invalid()
        {
            var notifiable = new FakeNotifiable();

            var failures = new List<ValidationFailure>
        {
            new ValidationFailure("Name", "Name is required"),
            new ValidationFailure("Age", "Age must be greater than 18")
        };
            var validationResult = new ValidationResult(failures);

            notifiable.AddNotifications(validationResult);

            Assert.Equal(2, notifiable.Notifications.Count);
            Assert.Contains(notifiable.Notifications, n => n.Key == "Name" && n.Message == "Name is required");
            Assert.Contains(notifiable.Notifications, n => n.Key == "Age" && n.Message == "Age must be greater than 18");
        }

        [Fact(DisplayName = "AddNotifications does not add notifications when validation result is valid")]
        public void AddNotifications_Should_NotAddNotifications_When_Valid()
        {
            var notifiable = new FakeNotifiable();
            var validationResult = new ValidationResult();

            notifiable.AddNotifications(validationResult);

            Assert.Empty(notifiable.Notifications);
        }
    }
}