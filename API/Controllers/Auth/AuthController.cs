using System.Security.Claims;
using Infrastructure.Auth.DTOs;
using Infrastructure.Auth.Interfaces;
using Infrastructure.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace API.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null) return Unauthorized();

        var user = await _authService.GetUserByIdAsync(userId);
        if (user == null) return NotFound();

        return Ok(new { user.Id, user.FullName, user.PhoneNumber, user.Email });
    }

    [Authorize(Roles = "Admin,Manager")]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!ApplicationRole.Roles.All.Contains(dto.Role))
            return BadRequest($"Invalid role '{dto.Role}'. Valid roles are: {string.Join(", ", ApplicationRole.Roles.All)}");

        var callerRole = User.FindFirst(ClaimTypes.Role)?.Value;
        if (callerRole == ApplicationRole.Roles.Manager && !ApplicationRole.Roles.ManagerCanCreate.Contains(dto.Role))
            return Forbid();

        var result = await _authService.RegisterAsync(dto);
        if (!result.Succeeded)
            return BadRequest(new { errors = result.Errors });

        return Ok("User registered successfully.");
    }

    [EnableRateLimiting("login")]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.LoginAsync(dto);
        if (!result.Succeeded)
            return Unauthorized(new ProblemDetails { Title = result.Errors[0], Status = 401 });

        return Ok(result.Data);
    }

    [EnableRateLimiting("standard")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] RefreshTokenDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.RefreshTokenAsync(dto.RefreshToken);
        if (!result.Succeeded)
            return Unauthorized(new ProblemDetails { Title = result.Errors[0], Status = 401 });

        return Ok(result.Data);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout([FromBody] RefreshTokenDto dto)
    {
        await _authService.RevokeTokenAsync(dto.RefreshToken);
        return Ok("Logged out successfully.");
    }

    [EnableRateLimiting("forgot-password")]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.ForgotPasswordAsync(dto);
        if (!result.Succeeded)
            return StatusCode(503, new ProblemDetails { Title = result.Errors[0], Status = 503 });

        return Ok("If the phone number is registered, a reset code has been sent via SMS.");
    }

    [EnableRateLimiting("standard")]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordPhone([FromBody] ResetPasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.ResetPasswordAsync(dto);
        if (!result.Succeeded)
            return BadRequest(new ProblemDetails { Title = result.Errors[0], Status = 400 });

        return Ok("Password has been reset successfully.");
    }

    [Authorize]
    [HttpPut("change-password")]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var result = await _authService.ChangePasswordAsync(userId, dto);
        if (!result.Succeeded)
            return BadRequest(new { errors = result.Errors });

        return Ok("Password changed successfully.");
    }

    [Authorize]
    [HttpPut("update")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId)) return Unauthorized();

        var result = await _authService.UpdateUserAsync(userId, dto);
        if (!result.Succeeded)
            return BadRequest(new { errors = result.Errors });

        return Ok("User updated successfully.");
    }
}
