using Application.Common;

namespace Application.Auth;

public interface IAuthService
{
    Task<Result> RegisterAsync(RegisterUserDto dto);
    Task<Result<LoginResponseDto>> LoginAsync(LoginUserDto dto);
    Task<Result<LoginResponseDto>> RefreshTokenAsync(string refreshToken);
    Task<Result> RevokeTokenAsync(string refreshToken);
    Task<Result> ForgotPasswordAsync(ForgotPasswordDto dto);
    Task<Result> ResetPasswordAsync(ResetPasswordDto dto);
}
