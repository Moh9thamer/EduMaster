using Application.Common;
using Domain.AcademicYears;

namespace Application.AcademicYears;

public interface IAcademicYearRepository : IRepository<AcademicYear>
{
    Task<(IList<AcademicYear> Items, int TotalCount)> GetPagedAsync(int page, int pageSize);
    Task<AcademicYear?> GetCurrentAsync();
    Task<bool> HasOverlappingNameAsync(string name, int? excludeId = null);
    Task ClearCurrentAsync();
}
