using Application.Common;

namespace Application.Users;

public interface IUserService
{
    Task<PaginatedResult<UserDto>> GetAllAsync(int page = 1, int pageSize = 20, bool includeInactive = false);
    Task<Result<UserDto>> GetByIdAsync(string userId);
    Task<Result> DeactivateAsync(string userId);
    Task<Result> ActivateAsync(string userId);
    Task<Result> UpdateUserAsync(string userId, UpdateUserDto dto);
    Task<Result> ChangePasswordAsync(string userId, ChangePasswordDto dto);
    Task<Result> AdminResetPasswordAsync(string userId, string newPassword);
}
