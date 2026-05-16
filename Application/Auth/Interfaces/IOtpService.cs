namespace Application.Auth;

public interface IOtpService
{
    Task<string> GenerateAsync(string phoneNumber);
    Task<bool> ValidateAsync(string phoneNumber, string code);
    Task InvalidateAsync(string phoneNumber);
}
