namespace DotnetBaseKit.Components.Shared.Notifications
{
    public class NotificationContext
    {
        private readonly List<Notification> _notifications;
        public IReadOnlyCollection<Notification> Notifications => _notifications;
        public bool HasNotifications => _notifications.Any();

        public NotificationContext()
        {
            _notifications = new List<Notification>();
        }

        public void AddNotification(string key, string message)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(key);
            ArgumentException.ThrowIfNullOrWhiteSpace(message);
            _notifications.Add(new Notification(key, message));
        }

        public void AddNotification(string message)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(message);
            _notifications.Add(new Notification(message));
        }

        public void AddNotification(Notification notification)
        {
            ArgumentNullException.ThrowIfNull(notification);
            _notifications.Add(notification);
        }

        public void AddNotifications(IReadOnlyCollection<Notification> notifications)
        {
            ArgumentNullException.ThrowIfNull(notifications);
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(List<Notification> notifications)
        {
            ArgumentNullException.ThrowIfNull(notifications);
            _notifications.AddRange(notifications);
        }
    }

}
