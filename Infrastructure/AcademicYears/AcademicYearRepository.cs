using Application.AcademicYears;
using Domain.AcademicYears;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.AcademicYears;

public class AcademicYearRepository(ApplicationDbContext db)
    : Repository<AcademicYear>(db), IAcademicYearRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<(IList<AcademicYear> Items, int TotalCount)> GetPagedAsync(int page, int pageSize)
    {
        var query = _db.AcademicYears.OrderByDescending(a => a.StartDate);
        var total = await query.CountAsync();
        var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        return (items, total);
    }

    public async Task<AcademicYear?> GetCurrentAsync() =>
        await _db.AcademicYears.FirstOrDefaultAsync(a => a.IsCurrent);

    public async Task<bool> HasOverlappingNameAsync(string name, int? excludeId = null) =>
        await _db.AcademicYears.AnyAsync(a => a.Name == name && (excludeId == null || a.Id != excludeId));

    public async Task ClearCurrentAsync() =>
        await _db.AcademicYears
            .Where(a => a.IsCurrent)
            .ExecuteUpdateAsync(s => s.SetProperty(a => a.IsCurrent, false));
}
