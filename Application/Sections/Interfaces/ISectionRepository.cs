using Application.Common;
using Domain.Sections;

namespace Application.Sections;

public interface ISectionRepository : IRepository<Section>
{
    Task<IList<Section>> GetByGradeAndYearAsync(int gradeId, int academicYearId);
    Task<bool> ExistsWithNameInGradeYearAsync(int gradeId, int academicYearId, string name, int? excludeId = null);
}
