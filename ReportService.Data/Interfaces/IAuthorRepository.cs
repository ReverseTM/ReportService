namespace ReportService.Data.Interfaces;

public interface IAuthorRepository<T> where T : class
{
    Task<T?> GetById(long id);
    Task<T?> GetByEmail(string email);
    Task<IEnumerable<T>> GetAll();
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(long id);
}