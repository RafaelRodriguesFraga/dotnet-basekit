using FluentValidation.Results;
using DotnetBaseKit.Components.Shared.Notifications;

namespace DotnetBaseKit.Components.Domain.MongoDb.Entities.Base
{
    public abstract class BaseEntity : Notifiable<Notification>, IBaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }
        public Guid Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; }

        public abstract void Validate();

    }
}
