using DotnetBaseKit.Components.Shared.Notifications;
using FluentValidation.Results;

namespace DotnetBaseKit.Components.Domain.MongoDb
{
    public static class DomainMongoExtensions
    {
        public static void AddNotifications(this Notifiable<Notification> notifiable, ValidationResult result)
        {
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    notifiable.AddNotification(failure.PropertyName, failure.ErrorMessage);
                }
            }
        }
    }
}