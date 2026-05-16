using Application.GradingSchemes;
using Domain.GradingSchemes;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.GradingSchemes;

public class GradingSchemeRepository(ApplicationDbContext db)
    : Repository<GradingScheme>(db), IGradingSchemeRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<IList<GradingScheme>> GetBySubjectAndSemesterAsync(int subjectId, int semesterId) =>
        await _db.GradingSchemes
            .Where(g => g.SubjectId == subjectId && g.SemesterId == semesterId)
            .ToListAsync();

    public async Task<GradingScheme?> GetBySubjectAndSemesterSingleAsync(int subjectId, int semesterId, int? excludeId = null) =>
        await _db.GradingSchemes.FirstOrDefaultAsync(g =>
            g.SubjectId == subjectId &&
            g.SemesterId == semesterId &&
            (excludeId == null || g.Id != excludeId));
}
