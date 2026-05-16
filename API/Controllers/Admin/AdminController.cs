using Application.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin")]
public class AdminController(IUserService userService) : ControllerBase
{
    [HttpGet("users")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] bool includeInactive = false)
    {
        var result = await userService.GetAllAsync(page, pageSize, includeInactive);
        return Ok(result);
    }

    [HttpGet("users/{userId}")]
    public async Task<IActionResult> GetUser(string userId)
    {
        var result = await userService.GetByIdAsync(userId);
        if (!result.Succeeded)
            return NotFound(new ProblemDetails { Title = result.Errors[0], Status = 404 });

        return Ok(result.Data);
    }

    [HttpDelete("users/{userId}")]
    public async Task<IActionResult> DeactivateUser(string userId)
    {
        var result = await userService.DeactivateAsync(userId);
        if (!result.Succeeded)
            return NotFound(new ProblemDetails { Title = result.Errors[0], Status = 404 });

        return Ok("User deactivated.");
    }

    [HttpPut("users/{userId}/activate")]
    public async Task<IActionResult> ActivateUser(string userId)
    {
        var result = await userService.ActivateAsync(userId);
        if (!result.Succeeded)
            return NotFound(new ProblemDetails { Title = result.Errors[0], Status = 404 });

        return Ok("User activated.");
    }

    [HttpPut("users/{userId}/reset-password")]
    public async Task<IActionResult> ResetUserPassword(string userId, [FromBody] AdminResetPasswordDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await userService.AdminResetPasswordAsync(userId, dto.NewPassword);
        if (!result.Succeeded)
            return BadRequest(new { errors = result.Errors });

        return Ok("Password reset successfully.");
    }
}
