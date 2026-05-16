using Domain.Exceptions;

namespace Domain.Semesters;

public class Semester
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int AcademicYearId { get; private set; }
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public bool IsActive { get; private set; } = true;

    private Semester() { }

    public static Semester Create(string name, int academicYearId, DateOnly startDate, DateOnly endDate)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Semester name is required.");
        if (endDate <= startDate)
            throw new ValidationException("End date must be after start date.");

        return new Semester
        {
            Name = name,
            AcademicYearId = academicYearId,
            StartDate = startDate,
            EndDate = endDate
        };
    }

    public void Update(string? name, DateOnly? startDate, DateOnly? endDate)
    {
        var newStart = startDate ?? StartDate;
        var newEnd = endDate ?? EndDate;

        if (newEnd <= newStart)
            throw new ValidationException("End date must be after start date.");

        if (name is not null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Semester name is required.");
            Name = name;
        }

        StartDate = newStart;
        EndDate = newEnd;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new ConflictException("Semester is already inactive.");
        IsActive = false;
    }
}
