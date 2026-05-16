using System.ComponentModel.DataAnnotations;

namespace Application.Subjects;

public class UpdateSubjectDto
{
    [MaxLength(100)]
    public string? Name { get; set; }
}
