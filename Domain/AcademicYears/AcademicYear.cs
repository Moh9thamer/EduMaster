using Domain.Exceptions;

namespace Domain.AcademicYears;

public class AcademicYear
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public DateOnly StartDate { get; private set; }
    public DateOnly EndDate { get; private set; }
    public bool IsCurrent { get; private set; }
    public bool IsActive { get; private set; } = true;

    private AcademicYear() { }

    public static AcademicYear Create(string name, DateOnly startDate, DateOnly endDate)
    {
        if (endDate <= startDate)
            throw new ValidationException("End date must be after start date.");

        return new AcademicYear
        {
            Name = name,
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

        if (name is not null) Name = name;
        StartDate = newStart;
        EndDate = newEnd;
    }

    public void MarkAsCurrent() => IsCurrent = true;

    public void ClearCurrent() => IsCurrent = false;

    public void Deactivate()
    {
        if (!IsActive)
            throw new ConflictException("Academic year is already inactive.");

        if (IsCurrent)
            throw new ConflictException("Cannot deactivate the current academic year. Set another year as current first.");

        IsActive = false;
    }
}
