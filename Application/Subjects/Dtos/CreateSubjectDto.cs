using System.ComponentModel.DataAnnotations;

namespace Application.Subjects;

public class CreateSubjectDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int GradeId { get; set; }
}
