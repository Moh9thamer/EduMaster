using Domain.Exceptions;

namespace Domain.Grades;

public class Grade
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int Level { get; private set; }
    public bool IsFinal { get; private set; }
    public bool IsActive { get; private set; } = true;

    private Grade() { }

    public static Grade Create(string name, int level, bool isFinal = false)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ValidationException("Grade name is required.");
        if (level < 1)
            throw new ValidationException("Level must be greater than 0.");

        return new Grade { Name = name, Level = level, IsFinal = isFinal };
    }

    public void Update(string? name, int? level, bool? isFinal)
    {
        if (name is not null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException("Grade name is required.");
            Name = name;
        }

        if (level is not null)
        {
            if (level < 1)
                throw new ValidationException("Level must be greater than 0.");
            Level = level.Value;
        }

        if (isFinal is not null)
            IsFinal = isFinal.Value;
    }

    public void Deactivate()
    {
        if (!IsActive)
            throw new ConflictException("Grade is already inactive.");
        IsActive = false;
    }
}
