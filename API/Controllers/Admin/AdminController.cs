using Infrastructure.Auth.DTOs;
using Infrastructure.Auth.Interfaces;
using Infrastructure.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AdminController(IAuthService authService, IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }

    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] bool includeInactive = false)
    {
        var result = await _userService.GetAllAsync(page, pageSize, includeInactive);
        return Ok(result);
    }

    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetUser(string userId)
    {
        var result = await _userService.GetByIdAsync(userId);
        if (!result.Succeeded)
            return NotFound(new ProblemDetails { Title = result.Errors[0], Status = 404 });

        return Ok(result.Data);
    }

    [HttpDelete("users/{userId}")]
    public async Task<IActionResult> DeactivateUser(string userId)
    {
        var result = await _userService.DeactivateAsync(userId);
        if (!result.Succeeded)
            return NotFound(new ProblemDetails { Title = result.Errors[0], Status = 404 });

        return Ok("User deactivated.");
    }

    [HttpPut("users/{userId}/activate")]
    public async Task<IActionResult> ActivateUser(string userId)
    {
        var result = await _userService.ActivateAsync(userId);
        if (!result.Succeeded)
            return NotFound(new ProblemDetails { Title = result.Errors[0], Status = 404 });

        return Ok("User activated.");
    }

    [HttpPut("users/{userId}/reset-password")]
    public async Task<IActionResult> ResetUserPassword(string userId, [FromBody] AdminResetPasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _authService.AdminResetPasswordAsync(userId, dto.NewPassword);
        if (!result.Succeeded)
            return BadRequest(new { errors = result.Errors });

        return Ok("Password reset successfully.");
    }
}
