using Application.AcademicYears;
using Application.Common;
using Application.Grades;
using Application.Sections;
using Domain.Exceptions;
using Domain.Sections;

namespace Infrastructure.Sections;

public class SectionService(
    ISectionRepository repository,
    IGradeRepository gradeRepository,
    IAcademicYearRepository academicYearRepository) : ISectionService
{
    public async Task<IList<SectionDto>> GetByGradeAndYearAsync(int gradeId, int academicYearId)
    {
        var sections = await repository.GetByGradeAndYearAsync(gradeId, academicYearId);
        return sections.Select(MapToDto).ToList();
    }

    public async Task<Result<SectionDto>> GetByIdAsync(int id)
    {
        var section = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Section not found.");

        return Result<SectionDto>.Ok(MapToDto(section));
    }

    public async Task<Result<SectionDto>> CreateAsync(CreateSectionDto dto)
    {
        _ = await gradeRepository.GetByIdAsync(dto.GradeId)
            ?? throw new NotFoundException("Grade not found.");

        _ = await academicYearRepository.GetByIdAsync(dto.AcademicYearId)
            ?? throw new NotFoundException("Academic year not found.");

        if (await repository.ExistsWithNameInGradeYearAsync(dto.GradeId, dto.AcademicYearId, dto.Name))
            throw new ConflictException($"A section named '{dto.Name}' already exists in this grade and academic year.");

        var section = Section.Create(dto.Name, dto.GradeId, dto.AcademicYearId, dto.Capacity);

        await repository.AddAsync(section);
        await repository.SaveChangesAsync();

        return Result<SectionDto>.Ok(MapToDto(section));
    }

    public async Task<Result<SectionDto>> UpdateAsync(int id, UpdateSectionDto dto)
    {
        var section = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Section not found.");

        if (dto.Name is not null && dto.Name != section.Name)
            if (await repository.ExistsWithNameInGradeYearAsync(section.GradeId, section.AcademicYearId, dto.Name, excludeId: id))
                throw new ConflictException($"A section named '{dto.Name}' already exists in this grade and academic year.");

        section.Update(dto.Name, dto.Capacity);

        repository.Update(section);
        await repository.SaveChangesAsync();

        return Result<SectionDto>.Ok(MapToDto(section));
    }

    public async Task<Result> DeactivateAsync(int id)
    {
        var section = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException("Section not found.");

        section.Deactivate();

        repository.Update(section);
        await repository.SaveChangesAsync();

        return Result.Ok();
    }

    private static SectionDto MapToDto(Section section) => new()
    {
        Id = section.Id,
        Name = section.Name,
        GradeId = section.GradeId,
        AcademicYearId = section.AcademicYearId,
        Capacity = section.Capacity,
        IsActive = section.IsActive
    };
}
