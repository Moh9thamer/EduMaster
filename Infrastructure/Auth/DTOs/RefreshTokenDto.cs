using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Auth.DTOs;

public class RefreshTokenDto
{
    [Required]
    public string RefreshToken { get; set; } = string.Empty;
}
