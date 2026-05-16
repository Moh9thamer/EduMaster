namespace Application.Notifications;

public interface ISmsService
{
    Task SendAsync(string toPhoneNumber, string message);
}
