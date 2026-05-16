using System.ComponentModel.DataAnnotations;

namespace Application.Users;

public class UpdateUserDto
{
    public string? FullName { get; set; }

    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
}
