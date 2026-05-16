namespace Application.Common;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(object id);
    Task<IList<T>> GetAllAsync();
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveChangesAsync();
}
