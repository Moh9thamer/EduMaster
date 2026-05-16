using Application.Common;

namespace Application.GradingSchemes;

public interface IGradingSchemeService
{
    Task<IList<GradingSchemeDto>> GetBySubjectAndSemesterAsync(int subjectId, int semesterId);
    Task<Result<GradingSchemeDto>> GetByIdAsync(int id);
    Task<Result<GradingSchemeDto>> CreateAsync(CreateGradingSchemeDto dto);
    Task<Result<GradingSchemeDto>> UpdateAsync(int id, UpdateGradingSchemeDto dto);
    Task<Result> DeactivateAsync(int id);
}
