using Application.Common;

namespace Application.Sections;

public interface ISectionService
{
    Task<IList<SectionDto>> GetByGradeAndYearAsync(int gradeId, int academicYearId);
    Task<Result<SectionDto>> GetByIdAsync(int id);
    Task<Result<SectionDto>> CreateAsync(CreateSectionDto dto);
    Task<Result<SectionDto>> UpdateAsync(int id, UpdateSectionDto dto);
    Task<Result> DeactivateAsync(int id);
}
