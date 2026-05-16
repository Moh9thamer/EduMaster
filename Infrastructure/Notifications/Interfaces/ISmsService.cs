namespace Infrastructure.Notifications.Interfaces;

public interface ISmsService
{
    Task SendAsync(string toPhoneNumber, string message);
}