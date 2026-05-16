using System.ComponentModel.DataAnnotations;

namespace Application.AcademicYears;

public class UpdateAcademicYearDto
{
    [MaxLength(100)]
    public string? Name { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}
