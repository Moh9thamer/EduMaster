using System.ComponentModel.DataAnnotations;

namespace Application.Sections;

public class UpdateSectionDto
{
    [MaxLength(50)]
    public string? Name { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
    public int? Capacity { get; set; }
}
