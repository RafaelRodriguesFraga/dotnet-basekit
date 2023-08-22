using DotnetBoilerplate.Components.Shared.Notifications;

namespace DotnetBoilerplate.Components.Application.Base
{
    public class BaseServiceApplication : IBaseServiceApplication
    {
        protected readonly NotificationContext _notificationContext;
        public BaseServiceApplication(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
        }
    }
}
