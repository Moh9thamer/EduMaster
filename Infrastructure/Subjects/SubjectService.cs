using Application.Common;
using Application.Grades;
using Application.Subjects;
using Domain.Exceptions;
using Domain.Subjects;

namespace Infrastructure.Subjects;

public class SubjectService(
    ISubjectRepository repository,
    IGradeRepository gradeRepository) : ISubjectService
{
    public async Task<IList<SubjectDto>> GetByGradeAsync(int gradeId)
    {
        var subjects = await repository.GetByGradeAsync(gradeId);
        return subjects.Select(MapToDto).ToList();
    }

    public async Task<Result<SubjectDto>> GetByIdAsync(int id)
    {
        var subject = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Subject not found.");

        return Result<SubjectDto>.Ok(MapToDto(subject));
    }

    public async Task<Result<SubjectDto>> CreateAsync(CreateSubjectDto dto)
    {
        _ = await gradeRepository.GetByIdAsync(dto.GradeId)
            ?? throw new NotFoundException("Grade not found.");

        if (await repository.ExistsWithNameInGradeAsync(dto.GradeId, dto.Name))
            throw new ConflictException($"A subject named '{dto.Name}' already exists in this grade.");

        var subject = Subject.Create(dto.Name, dto.GradeId);

        await repository.AddAsync(subject);
        await repository.SaveChangesAsync();

        return Result<SubjectDto>.Ok(MapToDto(subject));
    }

    public async Task<Result<SubjectDto>> UpdateAsync(int id, UpdateSubjectDto dto)
    {
        var subject = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Subject not found.");

        if (dto.Name is not null && dto.Name != subject.Name)
            if (await repository.ExistsWithNameInGradeAsync(subject.GradeId, dto.Name, excludeId: id))
                throw new ConflictException($"A subject named '{dto.Name}' already exists in this grade.");

        subject.Update(dto.Name);

        repository.Update(subject);
        await repository.SaveChangesAsync();

        return Result<SubjectDto>.Ok(MapToDto(subject));
    }

    public async Task<Result> DeactivateAsync(int id)
    {
        var subject = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Subject not found.");

        subject.Deactivate();

        repository.Update(subject);
        await repository.SaveChangesAsync();

        return Result.Ok();
    }

    private static SubjectDto MapToDto(Subject subject) => new()
    {
        Id = subject.Id,
        Name = subject.Name,
        GradeId = subject.GradeId,
        IsActive = subject.IsActive
    };
}
