using Infrastructure.Auth.DTOs;
using Infrastructure.User;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Auth.Interfaces;

public interface IAuthService
{
    Task<IdentityResult> RegisterAsync(RegisterUserDto dto);
    Task<LoginResponseDto?> LoginAsync(LoginUserDto dto);
    Task<LoginResponseDto?> RefreshTokenAsync(string refreshToken);
    Task<bool> RevokeTokenAsync(string refreshToken);

    Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto);
    Task<bool> ResetPasswordAsync(ResetPasswordDto dto);

    Task<ApplicationUser?> GetUserByIdAsync(string userId);
    Task<IdentityResult> UpdateUserAsync(string userId, UpdateUserDto dto);
    Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordDto dto);
    Task<IdentityResult> AdminResetPasswordAsync(string userId, string newPassword);
}