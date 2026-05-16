using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Auth.DTOs;

public class ForgotPasswordDto
{
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
}
