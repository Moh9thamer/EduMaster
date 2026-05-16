using Domain.Exceptions;

namespace Domain.Sections;

public class Section
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int GradeId { get; private set; }
    public int AcademicYearId { get; private set; }
    public int Capacity { get; private set; }
    public bool IsActive { get; private set; } = true;

    private Section() { }

    public static Section Create(string name, int gradeId, int academicYearId, int capacity)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Section name is required.");
        if (capacity <= 0)
            throw new ValidationException("Capacity must be greater than zero.");

        return new Section
        {
            Name = name.Trim(),
            GradeId = gradeId,
            AcademicYearId = academicYearId,
            Capacity = capacity
        };
    }

    public void Update(string? name, int? capacity)
    {
        if (name is not null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Section name cannot be empty.");
            Name = name.Trim();
        }

        if (capacity is not null)
        {
            if (capacity <= 0)
                throw new ValidationException("Capacity must be greater than zero.");
            Capacity = capacity.Value;
        }
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new ConflictException("Section is already inactive.");
        IsActive = false;
    }
}
