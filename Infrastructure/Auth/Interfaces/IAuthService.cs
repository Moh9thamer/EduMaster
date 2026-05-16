using Application.Common;
using Infrastructure.Auth.DTOs;
using Infrastructure.User;

namespace Infrastructure.Auth.Interfaces;

public interface IAuthService
{
    Task<Result> RegisterAsync(RegisterUserDto dto);
    Task<Result<LoginResponseDto>> LoginAsync(LoginUserDto dto);
    Task<Result<LoginResponseDto>> RefreshTokenAsync(string refreshToken);
    Task<Result> RevokeTokenAsync(string refreshToken);

    Task<Result> ForgotPasswordAsync(ForgotPasswordDto dto);
    Task<Result> ResetPasswordAsync(ResetPasswordDto dto);

    Task<ApplicationUser?> GetUserByIdAsync(string userId);
    Task<Result> UpdateUserAsync(string userId, UpdateUserDto dto);
    Task<Result> ChangePasswordAsync(string userId, ChangePasswordDto dto);
    Task<Result> AdminResetPasswordAsync(string userId, string newPassword);
}
