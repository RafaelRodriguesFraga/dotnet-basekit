using FluentValidation.Results;
using DotnetBaseKit.Components.Shared.Notifications;

namespace DotnetBaseKit.Components.Domain.Dtos.Base
{
    public abstract class BaseDto : Notifiable<Notification>, IBaseDto
    {
        public abstract void Validate();

        public void AddNotifications(ValidationResult validationResult)
        {
            var invalidValidation = !validationResult.IsValid;
            if (invalidValidation)
            {
                foreach (var failure in validationResult.Errors)
                {
                    AddNotification(failure.PropertyName, failure.ErrorMessage);
                }
            }

        }
    }
}
