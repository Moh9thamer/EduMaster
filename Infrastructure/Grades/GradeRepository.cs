using Application.Grades;
using Domain.Grades;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Grades;

public class GradeRepository(ApplicationDbContext db)
    : Repository<Grade>(db), IGradeRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<IList<Grade>> GetAllOrderedAsync() =>
        await _db.Grades.OrderBy(g => g.Level).ToListAsync();

    public async Task<bool> ExistsWithLevelAsync(int level, int? excludeId = null) =>
        await _db.Grades.AnyAsync(g => g.Level == level && (excludeId == null || g.Id != excludeId));

    public async Task<bool> ExistsWithNameAsync(string name, int? excludeId = null) =>
        await _db.Grades.AnyAsync(g => g.Name == name && (excludeId == null || g.Id != excludeId));
}
