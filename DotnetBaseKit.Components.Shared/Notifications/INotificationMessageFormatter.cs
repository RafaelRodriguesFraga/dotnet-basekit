namespace DotnetBaseKit.Components.Shared.Notifications
{
    public interface INotificationMessageFormatter
    {
        List<string> Format(IReadOnlyCollection<Notification> notifications, bool includeKey = true);
    }
}