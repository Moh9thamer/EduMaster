using System.ComponentModel.DataAnnotations;

namespace Application.Auth;

public class ForgotPasswordDto
{
    [Required]
    [Phone]
    public string PhoneNumber { get; set; } = string.Empty;
}
