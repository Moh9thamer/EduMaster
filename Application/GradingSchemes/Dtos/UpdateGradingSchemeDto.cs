using System.ComponentModel.DataAnnotations;

namespace Application.GradingSchemes;

public class UpdateGradingSchemeDto
{
    [Range(0, 100)]
    public decimal? QuizWeight { get; set; }

    [Range(0, 100)]
    public decimal? MidtermWeight { get; set; }

    [Range(0, 100)]
    public decimal? FinalWeight { get; set; }

    [Range(0, 100)]
    public decimal? AssignmentWeight { get; set; }

    [Range(0, 100)]
    public decimal? AttendanceWeight { get; set; }
}
