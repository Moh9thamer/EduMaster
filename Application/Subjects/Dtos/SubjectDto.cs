namespace Application.Subjects;

public class SubjectDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int GradeId { get; set; }
    public bool IsActive { get; set; }
}
