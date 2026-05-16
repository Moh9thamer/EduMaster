using Application.Common;

namespace Application.Semesters;

public interface ISemesterService
{
    Task<IList<SemesterDto>> GetByAcademicYearAsync(int academicYearId);
    Task<Result<SemesterDto>> GetByIdAsync(int id);
    Task<Result<SemesterDto>> CreateAsync(CreateSemesterDto dto);
    Task<Result<SemesterDto>> UpdateAsync(int id, UpdateSemesterDto dto);
    Task<Result> DeactivateAsync(int id);
}
