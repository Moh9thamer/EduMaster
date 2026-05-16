namespace Infrastructure.User;

public interface IUserService
{
    Task<IList<UserDto>> GetAllAsync();
    Task<UserDto?> GetByIdAsync(string userId);
    Task<bool> DeleteAsync(string userId);
}
