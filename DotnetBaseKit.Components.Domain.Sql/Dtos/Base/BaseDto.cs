using FluentValidation.Results;
using DotnetBaseKit.Components.Shared.Notifications;

namespace DotnetBaseKit.Components.Domain.Dtos.Base
{
    public abstract class BaseDto : Notifiable<Notification>, IBaseDto
    {
        public abstract void Validate();

    }
}
