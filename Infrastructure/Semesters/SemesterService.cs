using Application.AcademicYears;
using Application.Common;
using Application.Semesters;
using Domain.Exceptions;
using Domain.Semesters;

namespace Infrastructure.Semesters;

public class SemesterService(ISemesterRepository repository, IAcademicYearRepository academicYearRepository) : ISemesterService
{
    public async Task<IList<SemesterDto>> GetByAcademicYearAsync(int academicYearId)
    {
        var semesters = await repository.GetByAcademicYearAsync(academicYearId);
        return semesters.Select(MapToDto).ToList();
    }

    public async Task<Result<SemesterDto>> GetByIdAsync(int id)
    {
        var semester = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Semester not found.");

        return Result<SemesterDto>.Ok(MapToDto(semester));
    }

    public async Task<Result<SemesterDto>> CreateAsync(CreateSemesterDto dto)
    {
        var year = await academicYearRepository.GetByIdAsync(dto.AcademicYearId)
            ?? throw new NotFoundException("Academic year not found.");

        if (dto.StartDate < year.StartDate || dto.EndDate > year.EndDate)
            throw new ValidationException($"Semester dates must be within the academic year ({year.StartDate} – {year.EndDate}).");

        if (await repository.ExistsWithNameInYearAsync(dto.AcademicYearId, dto.Name))
            throw new ConflictException($"A semester named '{dto.Name}' already exists in this academic year.");

        if (await repository.HasOverlapAsync(dto.AcademicYearId, dto.StartDate, dto.EndDate))
            throw new ConflictException("Semester dates overlap with an existing semester in this academic year.");

        var semester = Semester.Create(dto.Name, dto.AcademicYearId, dto.StartDate, dto.EndDate);

        await repository.AddAsync(semester);
        await repository.SaveChangesAsync();

        return Result<SemesterDto>.Ok(MapToDto(semester));
    }

    public async Task<Result<SemesterDto>> UpdateAsync(int id, UpdateSemesterDto dto)
    {
        var semester = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Semester not found.");

        var newStart = dto.StartDate ?? semester.StartDate;
        var newEnd = dto.EndDate ?? semester.EndDate;

        var year = await academicYearRepository.GetByIdAsync(semester.AcademicYearId)
            ?? throw new NotFoundException("Academic year not found.");

        if (newStart < year.StartDate || newEnd > year.EndDate)
            throw new ValidationException($"Semester dates must be within the academic year ({year.StartDate} – {year.EndDate}).");

        if (dto.Name is not null && dto.Name != semester.Name)
            if (await repository.ExistsWithNameInYearAsync(semester.AcademicYearId, dto.Name, excludeId: id))
                throw new ConflictException($"A semester named '{dto.Name}' already exists in this academic year.");

        if (dto.StartDate is not null || dto.EndDate is not null)
            if (await repository.HasOverlapAsync(semester.AcademicYearId, newStart, newEnd, excludeId: id))
                throw new ConflictException("Semester dates overlap with an existing semester in this academic year.");

        semester.Update(dto.Name, dto.StartDate, dto.EndDate);

        repository.Update(semester);
        await repository.SaveChangesAsync();

        return Result<SemesterDto>.Ok(MapToDto(semester));
    }

    public async Task<Result> DeactivateAsync(int id)
    {
        var semester = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Semester not found.");

        semester.Deactivate();

        repository.Update(semester);
        await repository.SaveChangesAsync();

        return Result.Ok();
    }

    private static SemesterDto MapToDto(Semester semester) => new()
    {
        Id = semester.Id,
        Name = semester.Name,
        AcademicYearId = semester.AcademicYearId,
        StartDate = semester.StartDate,
        EndDate = semester.EndDate,
        IsActive = semester.IsActive
    };
}
