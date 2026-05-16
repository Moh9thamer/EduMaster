using Application.Common;
using Application.GradingSchemes;
using Application.Semesters;
using Application.Subjects;
using Domain.Exceptions;
using Domain.GradingSchemes;

namespace Infrastructure.GradingSchemes;

public class GradingSchemeService(
    IGradingSchemeRepository repository,
    ISubjectRepository subjectRepository,
    ISemesterRepository semesterRepository) : IGradingSchemeService
{
    public async Task<IList<GradingSchemeDto>> GetBySubjectAndSemesterAsync(int subjectId, int semesterId)
    {
        var schemes = await repository.GetBySubjectAndSemesterAsync(subjectId, semesterId);
        return schemes.Select(MapToDto).ToList();
    }

    public async Task<Result<GradingSchemeDto>> GetByIdAsync(int id)
    {
        var scheme = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Grading scheme not found.");

        return Result<GradingSchemeDto>.Ok(MapToDto(scheme));
    }

    public async Task<Result<GradingSchemeDto>> CreateAsync(CreateGradingSchemeDto dto)
    {
        _ = await subjectRepository.GetByIdAsync(dto.SubjectId)
            ?? throw new NotFoundException("Subject not found.");

        _ = await semesterRepository.GetByIdAsync(dto.SemesterId)
            ?? throw new NotFoundException("Semester not found.");

        if (await repository.GetBySubjectAndSemesterSingleAsync(dto.SubjectId, dto.SemesterId) is not null)
            throw new ConflictException("A grading scheme already exists for this subject and semester.");

        var scheme = GradingScheme.Create(
            dto.SubjectId,
            dto.SemesterId,
            dto.QuizWeight,
            dto.MidtermWeight,
            dto.FinalWeight,
            dto.AssignmentWeight,
            dto.AttendanceWeight);

        await repository.AddAsync(scheme);
        await repository.SaveChangesAsync();

        return Result<GradingSchemeDto>.Ok(MapToDto(scheme));
    }

    public async Task<Result<GradingSchemeDto>> UpdateAsync(int id, UpdateGradingSchemeDto dto)
    {
        var scheme = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Grading scheme not found.");

        scheme.Update(
            dto.QuizWeight,
            dto.MidtermWeight,
            dto.FinalWeight,
            dto.AssignmentWeight,
            dto.AttendanceWeight);

        repository.Update(scheme);
        await repository.SaveChangesAsync();

        return Result<GradingSchemeDto>.Ok(MapToDto(scheme));
    }

    public async Task<Result> DeactivateAsync(int id)
    {
        var scheme = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Grading scheme not found.");

        scheme.Deactivate();

        repository.Update(scheme);
        await repository.SaveChangesAsync();

        return Result.Ok();
    }

    private static GradingSchemeDto MapToDto(GradingScheme scheme) => new()
    {
        Id = scheme.Id,
        SubjectId = scheme.SubjectId,
        SemesterId = scheme.SemesterId,
        QuizWeight = scheme.QuizWeight,
        MidtermWeight = scheme.MidtermWeight,
        FinalWeight = scheme.FinalWeight,
        AssignmentWeight = scheme.AssignmentWeight,
        AttendanceWeight = scheme.AttendanceWeight,
        IsActive = scheme.IsActive
    };
}
