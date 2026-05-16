namespace Application.Sections;

public class SectionDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int GradeId { get; set; }
    public int AcademicYearId { get; set; }
    public int Capacity { get; set; }
    public bool IsActive { get; set; }
}
