using Application.Common;

namespace Application.AcademicYears;

public interface IAcademicYearService
{
    Task<PaginatedResult<AcademicYearDto>> GetAllAsync(int page, int pageSize);
    Task<Result<AcademicYearDto>> GetByIdAsync(int id);
    Task<Result<AcademicYearDto>> GetCurrentAsync();
    Task<Result<AcademicYearDto>> CreateAsync(CreateAcademicYearDto dto);
    Task<Result<AcademicYearDto>> UpdateAsync(int id, UpdateAcademicYearDto dto);
    Task<Result> SetCurrentAsync(int id);
    Task<Result> DeactivateAsync(int id);
}
