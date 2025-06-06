﻿namespace DotnetBaseKit.Components.Shared.Notifications
{
    public abstract class Notifiable<T> where T : Notification
    {
        private readonly List<T> _notifications;

        protected Notifiable() => _notifications = new List<T>();

        private T GetNotificationInstance(string key, string message)
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { key, message });
        }

        private T GetNotificationInstance(string message)
        {
            return (T)Activator.CreateInstance(typeof(T), new object[] { message });
        }


        public IReadOnlyCollection<T> Notifications => _notifications;

        public void AddNotification(string key, string message)
        {
            var notification = GetNotificationInstance(key, message);
            _notifications.Add(notification);
        }

        public void AddNotification(T notification)
        {
            _notifications.Add(notification);
        }

        public void AddNotification(string message)
        {
            var notification = GetNotificationInstance(message);
            _notifications.Add(notification);
        }

        public void AddNotifications(IReadOnlyCollection<T> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(List<T> notifications)
        {
            _notifications.AddRange(notifications);
        }

        public void AddNotifications(Notifiable<T> item)
        {
            AddNotifications(item.Notifications);
        }

        public void Clear()
        {
            _notifications.Clear();
        }

        public bool Valid => _notifications.Any() == false;

        public bool Invalid => !Valid;
    }
}
