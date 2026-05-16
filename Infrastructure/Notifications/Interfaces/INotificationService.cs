namespace Infrastructure.Notifications.Interfaces;

public interface INotificationService
{
    Task SendSmsAsync(string to, string message);
}