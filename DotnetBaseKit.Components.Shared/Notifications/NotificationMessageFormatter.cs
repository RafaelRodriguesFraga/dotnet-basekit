namespace DotnetBaseKit.Components.Shared.Notifications
{
    public class NotificationMessageFormatter : INotificationMessageFormatter
    {
        public List<string> Format(IReadOnlyCollection<Notification> notifications, bool includeKey = true)
        {
            var messages = new List<string>();

            foreach (var notify in notifications)
            {

                if (includeKey && !string.IsNullOrWhiteSpace(notify.Key))
                {

                    messages.Add($"{notify.Key}: {notify.Message}");
                }
                else
                {
                    messages.Add(notify.Message);
                }
            }

            return messages;
        }
    }
}