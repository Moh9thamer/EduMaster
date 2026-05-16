using System.ComponentModel.DataAnnotations;

namespace Application.Auth;

public class RefreshTokenDto
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
