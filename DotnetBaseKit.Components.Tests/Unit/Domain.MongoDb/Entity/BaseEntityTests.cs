using DotnetBaseKit.Components.Domain.MongoDb;
using DotnetBaseKit.Components.Tests.Mocks;
using FluentValidation.Results;

namespace DotnetBaseKit.Components.Tests.Unit.Domain.MongoDb.Entity
{
    public class BaseEntityTests
    {
        [Fact(DisplayName = "Should initialize Id and CreatedAt on constructor")]
        public void Should_Initialize_Id_And_CreatedAt_On_Constructor()
        {
            var entity = new FakeBaseEntity();

            Assert.NotEqual(Guid.Empty, entity.Id);
            Assert.True(entity.CreatedAt <= DateTime.Now);
        }

        [Fact(DisplayName = "Should add notifications from ValidationResult")]
        public void Should_Add_Notifications_From_ValidationResult()
        {
            var entity = new FakeBaseEntity();
            var validationResult = new ValidationResult(new List<ValidationFailure>
            {
                new ValidationFailure("Name", "Name is required"),
                new ValidationFailure("Email", "Email is invalid")
            });

            entity.AddNotifications(validationResult);

            Assert.False(entity.Valid);
            Assert.Equal(2, entity.Notifications.Count);
            Assert.Contains(entity.Notifications, n => n.Key == "Name" && n.Message == "Name is required");
            Assert.Contains(entity.Notifications, n => n.Key == "Email" && n.Message == "Email is invalid");
        }

        [Fact(DisplayName = "Validate should add notification when name is empty")]
        public void Validate_Should_AddNotification_When_Name_Is_Empty()
        {
            var entity = new FakeBaseEntityWithData("");
            entity.Validate();

            var notifications = entity.Notifications;

            Assert.Single(notifications);
            Assert.Equal("Name", notifications.First().Key);
            Assert.Equal("Name is required", notifications.First().Message);
        }

        [Fact(DisplayName = "Validate should not add notification when name is valid")]
        public void Validate_Should_NotAddNotification_When_NameIsValid()
        {
            var entity = new FakeBaseEntityWithData("Rafael");
            entity.Validate();

            var notifications = entity.Notifications;

            Assert.Empty(notifications);
        }
    }
}
