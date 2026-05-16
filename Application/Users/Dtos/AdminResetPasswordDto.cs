using System.ComponentModel.DataAnnotations;

namespace Application.Users;

public record AdminResetPasswordDto(
    [Required, MinLength(6)] string NewPassword
);
