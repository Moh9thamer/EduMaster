using System.ComponentModel.DataAnnotations;

namespace Application.Grades;

public class UpdateGradeDto
{
    [MaxLength(100)]
    public string? Name { get; set; }

    [Range(1, 100)]
    public int? Level { get; set; }

    public bool? IsFinal { get; set; }
}
