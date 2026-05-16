using Application.Common;
using Domain.Grades;

namespace Application.Grades;

public interface IGradeRepository : IRepository<Grade>
{
    Task<IList<Grade>> GetAllOrderedAsync();
    Task<bool> ExistsWithLevelAsync(int level, int? excludeId = null);
    Task<bool> ExistsWithNameAsync(string name, int? excludeId = null);
}
