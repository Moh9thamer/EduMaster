using Application.Common;

namespace Infrastructure.User;

public interface IUserService
{
    Task<PaginatedResult<UserDto>> GetAllAsync(int page = 1, int pageSize = 20, bool includeInactive = false);
    Task<Result<UserDto>> GetByIdAsync(string userId);
    Task<Result> DeactivateAsync(string userId);
    Task<Result> ActivateAsync(string userId);
}
