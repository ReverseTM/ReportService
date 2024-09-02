namespace ReportService.Data.Interfaces;

public interface IReportRepository<T> where T : class
{
    Task<T?> GetById(long id);
    Task<T?> GetByUrl(string url);
    Task<IEnumerable<T>> GetAll();
    Task Add(T entity);
    Task Update(T entity);
    Task Delete(long id);
}