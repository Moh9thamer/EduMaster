using Application.Common;
using Domain.Semesters;

namespace Application.Semesters;

public interface ISemesterRepository : IRepository<Semester>
{
    Task<IList<Semester>> GetByAcademicYearAsync(int academicYearId);
    Task<bool> HasOverlapAsync(int academicYearId, DateOnly startDate, DateOnly endDate, int? excludeId = null);
    Task<bool> ExistsWithNameInYearAsync(int academicYearId, string name, int? excludeId = null);
}
