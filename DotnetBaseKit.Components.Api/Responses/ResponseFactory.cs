using DotnetBaseKit.Components.Application.Pagination;
using DotnetBaseKit.Components.Shared.Notifications;

namespace DotnetBaseKit.Components.Api.Responses
{
    public class ResponseFactory : IResponseFactory
    {
        private readonly NotificationContext _notificationContext;
        public ResponseFactory(NotificationContext notificationContext)
        {
            _notificationContext = notificationContext;
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

        private static List<string> MessageResolver(IReadOnlyCollection<Notification> notifications)
        {
            var messages = new List<string>();

            foreach (var notify in notifications)
            {
                messages.Add($"{notify.Key}: {notify.Message}");
            }

            return messages;
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
