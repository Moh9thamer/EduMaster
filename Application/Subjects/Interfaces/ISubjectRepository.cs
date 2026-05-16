using Application.Common;
using Domain.Subjects;

namespace Application.Subjects;

public interface ISubjectRepository : IRepository<Subject>
{
    Task<IList<Subject>> GetByGradeAsync(int gradeId);
    Task<bool> ExistsWithNameInGradeAsync(int gradeId, string name, int? excludeId = null);
}
