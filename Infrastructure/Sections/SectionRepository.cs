using Application.Sections;
using Domain.Sections;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Sections;

public class SectionRepository(ApplicationDbContext db)
    : Repository<Section>(db), ISectionRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<IList<Section>> GetByGradeAndYearAsync(int gradeId, int academicYearId) =>
        await _db.Sections
            .Where(s => s.GradeId == gradeId && s.AcademicYearId == academicYearId)
            .OrderBy(s => s.Name)
            .ToListAsync();

    public async Task<bool> ExistsWithNameInGradeYearAsync(int gradeId, int academicYearId, string name, int? excludeId = null) =>
        await _db.Sections.AnyAsync(s =>
            s.GradeId == gradeId &&
            s.AcademicYearId == academicYearId &&
            s.Name == name &&
            (excludeId == null || s.Id != excludeId));
}
