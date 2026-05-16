using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Infrastructure.Auth.DTOs;
using Infrastructure.Auth.Interfaces;
using Infrastructure.Auth.Models;
using Infrastructure.Notifications.Interfaces;
using Infrastructure.Persistence;
using Infrastructure.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _config;
    private readonly INotificationService _notificationService;
    private readonly ApplicationDbContext _db;
    private readonly IOtpService _otpService;
    private readonly ILogger<AuthService> _logger;

    public AuthService(UserManager<ApplicationUser> userManager, IConfiguration config, INotificationService notificationService, ApplicationDbContext db, IOtpService otpService, ILogger<AuthService> logger)
    {
        _userManager = userManager;
        _config = config;
        _notificationService = notificationService;
        _db = db;
        _otpService = otpService;
        _logger = logger;
    }

        public async Task<IdentityResult> RegisterAsync(RegisterUserDto dto)
        {
            var phoneExists = await _userManager.Users
                .AnyAsync(u => u.PhoneNumber == dto.MobileNumber);

            if (phoneExists)
                return IdentityResult.Failed(new IdentityError { Description = "Phone number is already registered." });

            var user = new ApplicationUser
            {
                UserName = dto.Email,
                Email = dto.Email,
                FullName = dto.FullName,
                PhoneNumber = dto.MobileNumber
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded) return result;

            await _userManager.AddToRoleAsync(user, dto.Role);
            return result;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginUserDto dto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);

            if (user == null) return null;

            if (await _userManager.IsLockedOutAsync(user)) return null;

            if (!await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                await _userManager.AccessFailedAsync(user);
                return null;
            }

            await _userManager.ResetAccessFailedCountAsync(user);

            var jwt = await GenerateJwtAsync(user);
            var refreshToken = await CreateRefreshTokenAsync(user.Id);

            return new LoginResponseDto { Token = jwt, RefreshToken = refreshToken };
        }

        public async Task<LoginResponseDto?> RefreshTokenAsync(string refreshToken)
        {
            var stored = await _db.RefreshTokens
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Token == refreshToken);

            if (stored == null || !stored.IsValid) return null;

            stored.IsRevoked = true;

            var jwt = await GenerateJwtAsync(stored.User);
            var newRefreshToken = await CreateRefreshTokenAsync(stored.UserId);
            await _db.SaveChangesAsync();

            return new LoginResponseDto { Token = jwt, RefreshToken = newRefreshToken };
        }

        public async Task<bool> RevokeTokenAsync(string refreshToken)
        {
            var stored = await _db.RefreshTokens
                .FirstOrDefaultAsync(r => r.Token == refreshToken && !r.IsRevoked);

            if (stored == null) return false;

            stored.IsRevoked = true;
            await _db.SaveChangesAsync();
            return true;
        }

        private async Task<string> GenerateJwtAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.MobilePhone, user.PhoneNumber ?? ""),
                new Claim(ClaimTypes.Name, user.FullName ?? "")
            };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(5),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<string> CreateRefreshTokenAsync(string userId)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

            _db.RefreshTokens.Add(new RefreshToken
            {
                Token = token,
                UserId = userId,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            });

            await _db.SaveChangesAsync();
            return token;
        }

        public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto dto)
        {
            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);

            if (user == null) return false;

            var code = await _otpService.GenerateAsync(dto.PhoneNumber);

            try
            {
                await _notificationService.SendSmsAsync(dto.PhoneNumber, $"Your EduMaster reset code is: {code}. Valid for 10 minutes.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send OTP SMS to {Phone}", dto.PhoneNumber);
                await _otpService.InvalidateAsync(dto.PhoneNumber);
                return false;
            }

            return true;
        }

        public async Task<bool> ResetPasswordAsync(ResetPasswordDto dto)
        {
            if (!await _otpService.ValidateAsync(dto.PhoneNumber, dto.Code)) return false;

            var user = await _userManager.Users
                .FirstOrDefaultAsync(u => u.PhoneNumber == dto.PhoneNumber);

            if (user == null) return false;

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, dto.NewPassword);
            return result.Succeeded;
        }
        
        public async Task<ApplicationUser?> GetUserByIdAsync(string userId) =>
            await _userManager.FindByIdAsync(userId);

        public async Task<IdentityResult> UpdateUserAsync(string userId, UpdateUserDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            if (!string.IsNullOrWhiteSpace(dto.Email) && dto.Email != user.Email)
            {
                var emailExists = await _userManager.Users
                    .AnyAsync(u => u.NormalizedEmail == dto.Email.ToUpper() && u.Id != userId);
                if (emailExists)
                    return IdentityResult.Failed(new IdentityError { Description = "Email is already in use." });

                user.Email = dto.Email;
                user.UserName = dto.Email;
            }

            if (!string.IsNullOrWhiteSpace(dto.PhoneNumber) && dto.PhoneNumber != user.PhoneNumber)
            {
                var phoneExists = await _userManager.Users
                    .AnyAsync(u => u.PhoneNumber == dto.PhoneNumber && u.Id != userId);
                if (phoneExists)
                    return IdentityResult.Failed(new IdentityError { Description = "Phone number is already in use." });

                user.PhoneNumber = dto.PhoneNumber;
            }

            if (!string.IsNullOrWhiteSpace(dto.FullName))
                user.FullName = dto.FullName;

            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityResult> ChangePasswordAsync(string userId, ChangePasswordDto dto)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            return await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
        }

        public async Task<IdentityResult> AdminResetPasswordAsync(string userId, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return IdentityResult.Failed(new IdentityError { Description = "User not found." });

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return await _userManager.ResetPasswordAsync(user, token, newPassword);
        }
}