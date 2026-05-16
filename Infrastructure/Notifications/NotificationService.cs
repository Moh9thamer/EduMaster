using Application.Notifications;

namespace Infrastructure.Notifications;

public class NotificationService(ISmsService smsService) : INotificationService
{
    public Task SendSmsAsync(string to, string message) => smsService.SendAsync(to, message);
}
