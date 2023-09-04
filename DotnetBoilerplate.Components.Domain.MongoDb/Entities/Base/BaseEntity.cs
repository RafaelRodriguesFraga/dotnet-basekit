using FluentValidation.Results;
using DotnetBoilerplate.Components.Shared.Notifications;

namespace DotnetBoilerplate.Components.Domain.MongoDb.Entities.Base
{
    public abstract class BaseEntity : Notifiable<Notification>, IBaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }        
        public Guid Id  {get; protected set;}
        public DateTime CreatedAt {get; protected set;}

        public abstract void Validate();

        public void AddNotifications(ValidationResult validationResult)
        {
            var invalidValidation = !validationResult.IsValid;
            if(invalidValidation)
            {
                foreach(var failure in validationResult.Errors)
                {
                    AddNotification(failure.PropertyName, failure.ErrorMessage);
                }
            }
            
        }
    }
}
