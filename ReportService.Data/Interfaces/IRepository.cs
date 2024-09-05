namespace ReportService.Data.Interfaces;

/// <summary>
/// Generic repository interface that defines basic CRUD operations for entities of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The type of the entity managed by the repository.</typeparam>
public interface IRepository<T>
{
    /// <summary>
    /// Retrieves a specific entity of type <typeparamref name="T"/> from the repository.
    /// </summary>
    /// <param name="entity">The entity to retrieve, typically identified by its key properties.</param>
    /// <returns>A task representing an asynchronous operation that returns the retrieved entity.</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no author with the specified properties is found.</exception>
    Task<T> Get(T entity);
    
    /// <summary>
    /// Retrieves all entities of type <typeparamref name="T"/> from the repository.
    /// </summary>
    /// <returns>A task representing an asynchronous operation that returns an <see cref="IEnumerable{T}"/> containing all entities.</returns>
    Task<IEnumerable<T>> GetAll();
    
    /// <summary>
    /// Adds a new entity of type <typeparamref name="T"/> to the repository.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A task representing an asynchronous operation.</returns>
    Task Add(T entity);
    
    /// <summary>
    /// Adds multiple entities of type <typeparamref name="T"/> to the repository.
    /// </summary>
    /// <param name="entities">The collection of entities to add.</param>
    /// <returns>A task representing an asynchronous operation.</returns>
    Task AddRange(IEnumerable<T> entities);
    
    /// <summary>
    /// Updates an existing entity of type <typeparamref name="T"/> in the repository.
    /// </summary>
    /// <param name="entity">The entity with updated data.</param>
    /// <returns>A task representing an asynchronous operation.</returns>
    Task Update(T entity);
    
    /// <summary>
    /// Deletes an existing entity of type <typeparamref name="T"/> from the repository.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <returns>A task representing an asynchronous operation.</returns>
    Task Delete(T entity);
}