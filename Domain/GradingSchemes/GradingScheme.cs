using Domain.Exceptions;

namespace Domain.GradingSchemes;

public class GradingScheme
{
    public int Id { get; private set; }
    public int SubjectId { get; private set; }
    public int SemesterId { get; private set; }
    public decimal QuizWeight { get; private set; }
    public decimal MidtermWeight { get; private set; }
    public decimal FinalWeight { get; private set; }
    public decimal AssignmentWeight { get; private set; }
    public decimal AttendanceWeight { get; private set; }
    public bool IsActive { get; private set; } = true;

    private GradingScheme() { }

    public static GradingScheme Create(
        int subjectId,
        int semesterId,
        decimal quizWeight,
        decimal midtermWeight,
        decimal finalWeight,
        decimal assignmentWeight,
        decimal attendanceWeight)
    {
        ValidateWeights(quizWeight, midtermWeight, finalWeight, assignmentWeight, attendanceWeight);

        return new GradingScheme
        {
            SubjectId = subjectId,
            SemesterId = semesterId,
            QuizWeight = quizWeight,
            MidtermWeight = midtermWeight,
            FinalWeight = finalWeight,
            AssignmentWeight = assignmentWeight,
            AttendanceWeight = attendanceWeight
        };
    }

    public void Update(
        decimal? quizWeight,
        decimal? midtermWeight,
        decimal? finalWeight,
        decimal? assignmentWeight,
        decimal? attendanceWeight)
    {
        var newQuiz = quizWeight ?? QuizWeight;
        var newMidterm = midtermWeight ?? MidtermWeight;
        var newFinal = finalWeight ?? FinalWeight;
        var newAssignment = assignmentWeight ?? AssignmentWeight;
        var newAttendance = attendanceWeight ?? AttendanceWeight;

        ValidateWeights(newQuiz, newMidterm, newFinal, newAssignment, newAttendance);

        QuizWeight = newQuiz;
        MidtermWeight = newMidterm;
        FinalWeight = newFinal;
        AssignmentWeight = newAssignment;
        AttendanceWeight = newAttendance;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new ConflictException("Grading scheme is already inactive.");
        IsActive = false;
    }

    private static void ValidateWeights(
        decimal quiz, decimal midterm, decimal final, decimal assignment, decimal attendance)
    {
        if (quiz < 0 || midterm < 0 || final < 0 || assignment < 0 || attendance < 0)
            throw new ValidationException("Weights cannot be negative.");

        var total = quiz + midterm + final + assignment + attendance;
        if (total != 100)
            throw new ValidationException($"Weights must sum to 100. Current sum: {total}.");
    }
}
