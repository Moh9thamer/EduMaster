namespace Application.Grades;

public class GradeDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Level { get; set; }
    public bool IsFinal { get; set; }
    public bool IsActive { get; set; }
}
