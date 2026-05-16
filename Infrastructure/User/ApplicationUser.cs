using Microsoft.AspNetCore.Identity;

namespace Infrastructure.User;

public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}