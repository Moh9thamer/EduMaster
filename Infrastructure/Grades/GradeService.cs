using Application.Common;
using Application.Grades;
using Domain.Exceptions;
using Domain.Grades;

namespace Infrastructure.Grades;

public class GradeService(IGradeRepository repository) : IGradeService
{
    public async Task<IList<GradeDto>> GetAllAsync()
    {
        var grades = await repository.GetAllOrderedAsync();
        return grades.Select(MapToDto).ToList();
    }

    public async Task<Result<GradeDto>> GetByIdAsync(int id)
    {
        var grade = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Grade not found.");

        return Result<GradeDto>.Ok(MapToDto(grade));
    }

    public async Task<Result<GradeDto>> CreateAsync(CreateGradeDto dto)
    {
        if (await repository.ExistsWithLevelAsync(dto.Level))
            throw new ConflictException($"A grade with level {dto.Level} already exists.");

        if (await repository.ExistsWithNameAsync(dto.Name))
            throw new ConflictException($"A grade named '{dto.Name}' already exists.");

        var grade = Grade.Create(dto.Name, dto.Level, dto.IsFinal);

        await repository.AddAsync(grade);
        await repository.SaveChangesAsync();

        return Result<GradeDto>.Ok(MapToDto(grade));
    }

    public async Task<Result<GradeDto>> UpdateAsync(int id, UpdateGradeDto dto)
    {
        var grade = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Grade not found.");

        if (dto.Level is not null && dto.Level != grade.Level)
            if (await repository.ExistsWithLevelAsync(dto.Level.Value, excludeId: id))
                throw new ConflictException($"A grade with level {dto.Level} already exists.");

        if (dto.Name is not null && dto.Name != grade.Name)
            if (await repository.ExistsWithNameAsync(dto.Name, excludeId: id))
                throw new ConflictException($"A grade named '{dto.Name}' already exists.");

        grade.Update(dto.Name, dto.Level, dto.IsFinal);

        repository.Update(grade);
        await repository.SaveChangesAsync();

        return Result<GradeDto>.Ok(MapToDto(grade));
    }

    public async Task<Result> DeactivateAsync(int id)
    {
        var grade = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Grade not found.");

        grade.Deactivate();

        repository.Update(grade);
        await repository.SaveChangesAsync();

        return Result.Ok();
    }

    private static GradeDto MapToDto(Grade grade) => new()
    {
        Id = grade.Id,
        Name = grade.Name,
        Level = grade.Level,
        IsFinal = grade.IsFinal,
        IsActive = grade.IsActive
    };
}
