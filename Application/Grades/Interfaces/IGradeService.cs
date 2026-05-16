using Application.Common;

namespace Application.Grades;

public interface IGradeService
{
    Task<IList<GradeDto>> GetAllAsync();
    Task<Result<GradeDto>> GetByIdAsync(int id);
    Task<Result<GradeDto>> CreateAsync(CreateGradeDto dto);
    Task<Result<GradeDto>> UpdateAsync(int id, UpdateGradeDto dto);
    Task<Result> DeactivateAsync(int id);
}
