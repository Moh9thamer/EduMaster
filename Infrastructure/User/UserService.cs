using Application.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.User;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<PaginatedResult<UserDto>> GetAllAsync(int page = 1, int pageSize = 20, bool includeInactive = false)
    {
        var query = includeInactive
            ? _userManager.Users.IgnoreQueryFilters()
            : _userManager.Users;

        var totalCount = await query.CountAsync();

        var users = await query
            .OrderBy(u => u.FullName)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var items = new List<UserDto>();
        foreach (var user in users)
        {
            var roles = await _userManager.GetRolesAsync(user);
            items.Add(MapToDto(user, roles));
        }

        return new PaginatedResult<UserDto>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public async Task<Result<UserDto>> GetByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result<UserDto>.Fail("User not found.");

        var roles = await _userManager.GetRolesAsync(user);
        return Result<UserDto>.Ok(MapToDto(user, roles));
    }

    public async Task<Result> DeactivateAsync(string userId)
    {
        var user = await _userManager.Users
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) return Result.Fail("User not found.");
        if (!user.IsActive) return Result.Fail("User is already deactivated.");

        user.IsActive = false;
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded
            ? Result.Ok()
            : Result.Fail(result.Errors.Select(e => e.Description));
    }

    public async Task<Result> ActivateAsync(string userId)
    {
        var user = await _userManager.Users
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null) return Result.Fail("User not found.");
        if (user.IsActive) return Result.Fail("User is already active.");

        user.IsActive = true;
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded
            ? Result.Ok()
            : Result.Fail(result.Errors.Select(e => e.Description));
    }

    private static UserDto MapToDto(ApplicationUser user, IList<string> roles) => new()
    {
        Id = user.Id,
        FullName = user.FullName ?? string.Empty,
        Email = user.Email,
        PhoneNumber = user.PhoneNumber,
        Roles = roles,
        IsActive = user.IsActive
    };
}
