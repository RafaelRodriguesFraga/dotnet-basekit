using DotnetBaseKit.Components.Application.Base;
using DotnetBaseKit.Components.Shared.Notifications;

namespace DotnetBaseKit.Components.Tests.ServiceApplication;

public class BaseServiceApplicationTests
{
    [Fact(DisplayName = "Should instantiate BaseServiceApplication with NotificationContext")]
    public void Should_Instantiate_BaseServiceApplication_With_NotificationContext()
    {
        var notificationContext = new NotificationContext();

        var service = new BaseServiceApplication(notificationContext);

        // Usando reflection para acessar protected _notificationContext
        var field = typeof(BaseServiceApplication)
            .GetField("_notificationContext",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

        var contextValue = field.GetValue(service);

        Assert.NotNull(service);
        Assert.NotNull(contextValue);
        Assert.IsType<NotificationContext>(contextValue);
    }
}