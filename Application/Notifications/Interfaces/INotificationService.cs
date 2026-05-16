namespace Application.Notifications;

public interface INotificationService
{
    Task SendSmsAsync(string to, string message);
}
