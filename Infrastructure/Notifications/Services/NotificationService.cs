using Infrastructure.Notifications.Interfaces;

namespace Infrastructure.Notifications.Services;

public class NotificationService : INotificationService
{
    private readonly ISmsService _smsService;

    public NotificationService(ISmsService smsService)
    {
        _smsService = smsService;
    }

    public Task SendSmsAsync(string to, string message)
    {
        return _smsService.SendAsync(to, message);
    }
}