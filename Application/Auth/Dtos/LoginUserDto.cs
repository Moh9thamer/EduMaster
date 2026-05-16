using System.ComponentModel.DataAnnotations;

namespace Application.Auth;

public class LoginUserDto
{
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}
