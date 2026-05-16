using Application.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public class Repository<T>(ApplicationDbContext db) : IRepository<T> where T : class
{
    private readonly DbSet<T> _set = db.Set<T>();

    public async Task<T?> GetByIdAsync(object id) => await _set.FindAsync(id);

    public async Task<IList<T>> GetAllAsync() => await _set.ToListAsync();

    public async Task AddAsync(T entity) => await _set.AddAsync(entity);

    public void Update(T entity) => _set.Update(entity);

    public void Delete(T entity) => _set.Remove(entity);

    public async Task SaveChangesAsync() => await db.SaveChangesAsync();
}
