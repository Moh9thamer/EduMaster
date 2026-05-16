using Application.Common;
using Domain.GradingSchemes;

namespace Application.GradingSchemes;

public interface IGradingSchemeRepository : IRepository<GradingScheme>
{
    Task<IList<GradingScheme>> GetBySubjectAndSemesterAsync(int subjectId, int semesterId);
    Task<GradingScheme?> GetBySubjectAndSemesterSingleAsync(int subjectId, int semesterId, int? excludeId = null);
}
