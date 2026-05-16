using System.ComponentModel.DataAnnotations;

namespace Application.Sections;

public class CreateSectionDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int GradeId { get; set; }

    [Required]
    public int AcademicYearId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be greater than zero.")]
    public int Capacity { get; set; }
}
