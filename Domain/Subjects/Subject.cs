using Domain.Exceptions;

namespace Domain.Subjects;

public class Subject
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int GradeId { get; private set; }
    public bool IsActive { get; private set; } = true;

    private Subject() { }

    public static Subject Create(string name, int gradeId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Subject name is required.");

        return new Subject
        {
            Name = name.Trim(),
            GradeId = gradeId
        };
    }

    public void Update(string? name)
    {
        if (name is not null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Subject name cannot be empty.");
            Name = name.Trim();
        }
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new ConflictException("Subject is already inactive.");
        IsActive = false;
    }
}
