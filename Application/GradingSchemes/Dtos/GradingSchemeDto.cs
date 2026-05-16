namespace Application.GradingSchemes;

public class GradingSchemeDto
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public int SemesterId { get; set; }
    public decimal QuizWeight { get; set; }
    public decimal MidtermWeight { get; set; }
    public decimal FinalWeight { get; set; }
    public decimal AssignmentWeight { get; set; }
    public decimal AttendanceWeight { get; set; }
    public bool IsActive { get; set; }
}
