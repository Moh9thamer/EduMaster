using System.ComponentModel.DataAnnotations;

namespace Application.GradingSchemes;

public class CreateGradingSchemeDto
{
    [Required]
    public int SubjectId { get; set; }

    [Required]
    public int SemesterId { get; set; }

    [Required]
    [Range(0, 100)]
    public decimal QuizWeight { get; set; }

    [Required]
    [Range(0, 100)]
    public decimal MidtermWeight { get; set; }

    [Required]
    [Range(0, 100)]
    public decimal FinalWeight { get; set; }

    [Required]
    [Range(0, 100)]
    public decimal AssignmentWeight { get; set; }

    [Required]
    [Range(0, 100)]
    public decimal AttendanceWeight { get; set; }
}
