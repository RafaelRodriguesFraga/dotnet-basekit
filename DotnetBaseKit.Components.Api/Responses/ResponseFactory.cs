using DotnetBaseKit.Components.Application.Pagination;
using DotnetBaseKit.Components.Shared.Notifications;

namespace DotnetBaseKit.Components.Api.Responses
{
    public class ResponseFactory : IResponseFactory
    {
        private readonly NotificationContext _notificationContext;
        private readonly INotificationMessageFormatter _messageFormatter;
        private readonly bool _includeKey;
        public ResponseFactory(NotificationContext notificationContext, INotificationMessageFormatter messageFormatter, bool includeKey)
        {
            _notificationContext = notificationContext;
            _messageFormatter = messageFormatter;
            _includeKey = includeKey;
        }

        public Response Create()
        {
            return new Response(ResponseSuccess(), MessageResolver(_notificationContext.Notifications));
        }

        public Response<TData> Create<TData>(TData data) where TData : class
        {
            return new Response<TData>(data, ResponseSuccess(), MessageResolver(_notificationContext.Notifications));
        }

        private bool ResponseSuccess()
        {
            return !_notificationContext.HasNotifications;
        }


        private List<string> MessageResolver(IReadOnlyCollection<Notification> notifications)
        {

            return _messageFormatter.Format(notifications, _includeKey);
        }
        public ResponseList<TData> Create<TData>(IEnumerable<TData> dataList) where TData : class
        {
            return new ResponseList<TData>(dataList, ResponseSuccess(), MessageResolver(_notificationContext.Notifications));
        }

        public ResponsePaginated<TData> Create<TData>(PaginationResponse<TData> searchResult) where TData : class
        {
            return new ResponsePaginated<TData>(searchResult, ResponseSuccess(), MessageResolver(_notificationContext.Notifications));
        }
    }
}
