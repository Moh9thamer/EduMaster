using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Auth.DTOs;

public record AdminResetPasswordDto(
    [Required, MinLength(6)] string NewPassword
);
