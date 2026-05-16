using Application.Semesters;
using Domain.Semesters;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Semesters;

public class SemesterRepository(ApplicationDbContext db)
    : Repository<Semester>(db), ISemesterRepository
{
    private readonly ApplicationDbContext _db = db;

    public async Task<IList<Semester>> GetByAcademicYearAsync(int academicYearId) =>
        await _db.Semesters
            .Where(s => s.AcademicYearId == academicYearId)
            .OrderBy(s => s.StartDate)
            .ToListAsync();

    public async Task<bool> HasOverlapAsync(int academicYearId, DateOnly startDate, DateOnly endDate, int? excludeId = null) =>
        await _db.Semesters.AnyAsync(s =>
            s.AcademicYearId == academicYearId &&
            (excludeId == null || s.Id != excludeId) &&
            s.StartDate < endDate &&
            s.EndDate > startDate);

    public async Task<bool> ExistsWithNameInYearAsync(int academicYearId, string name, int? excludeId = null) =>
        await _db.Semesters.AnyAsync(s =>
            s.AcademicYearId == academicYearId &&
            s.Name == name &&
            (excludeId == null || s.Id != excludeId));
}
