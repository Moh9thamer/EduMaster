using System.ComponentModel.DataAnnotations;

namespace Application.AcademicYears;

public class CreateAcademicYearDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    public DateOnly EndDate { get; set; }

    public bool SetAsCurrent { get; set; }
}
