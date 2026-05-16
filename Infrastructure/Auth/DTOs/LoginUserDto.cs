using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Auth.DTOs;

public class LoginUserDto
{
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;
}