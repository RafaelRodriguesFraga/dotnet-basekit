namespace DotnetBaseKit.Components.Shared.Notifications
{
    public class Notification
    {

        public Notification(string? key, string message)
        {
            Key = key;
            Message = message;
        }

        public Notification(string message) : this(null, message)
        {
        }


        public string? Key { get; set; }
        public string Message { get; set; }
    }
}
