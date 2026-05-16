using Application.Subjects;
using Domain.Subjects;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Subjects;

public class SubjectRepository(ApplicationDbContext db)
    : Repository<Subject>(db), ISubjectRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<IList<Subject>> GetByGradeAsync(int gradeId) =>
        await _db.Subjects
            .Where(s => s.GradeId == gradeId)
            .OrderBy(s => s.Name)
            .ToListAsync();

    public async Task<bool> ExistsWithNameInGradeAsync(int gradeId, string name, int? excludeId = null) =>
        await _db.Subjects.AnyAsync(s =>
            s.GradeId == gradeId &&
            s.Name == name &&
            (excludeId == null || s.Id != excludeId));
}
