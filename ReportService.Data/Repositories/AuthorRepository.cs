using Microsoft.EntityFrameworkCore;
using ReportService.Data.DbContexts;
using ReportService.Data.Entities;
using ReportService.Data.Interfaces;

namespace ReportService.Data.Repositories;

/// <summary>
/// Repository for managing <see cref="AuthorEntity"/> entities.
/// Provides methods for adding, updating, deleting, and retrieving authors from the database.
/// </summary>
public sealed class AuthorRepository : IRepository<AuthorEntity>
{
    private readonly DbContextFactory _factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthorRepository"/> class with the specified <see cref="DbContextFactory"/>.
    /// </summary>
    /// <param name="factory">The factory used to create instances of the <see cref="ApplicationDbContext"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory"/> is null.</exception>
    public AuthorRepository(
        DbContextFactory factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }
    
    /// <inheritdoc cref="IRepository{T}.Get" />
    public async Task<AuthorEntity> Get(
        AuthorEntity authorEntity)
    {
        await using var context = _factory.Create();
        
        var author = await context.Authors
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Equals(authorEntity));
        
        if (author == null)
        {
            throw new KeyNotFoundException($"The author with id {authorEntity.Id} was not found.");
        }
        
        return author;
    }
    
    /// <inheritdoc cref="IRepository{T}.GetAll" />
    public async Task<IEnumerable<AuthorEntity>> GetAll()
    {
        await using var context = _factory.Create();
        
        return await context.Authors
            .AsNoTracking()
            .ToListAsync();
    }

    /// <inheritdoc cref="IRepository{T}.Add" />
    public async Task Add(
        AuthorEntity authorEntity)
    {
        await using var context = _factory.Create();
        
        await context.Authors.AddAsync(authorEntity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IRepository{T}.AddRange" />
    public async Task AddRange(
        IEnumerable<AuthorEntity> authorEntities)
    {
        await using var context = _factory.Create();
        
        await context.Authors.AddRangeAsync(authorEntities);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IRepository{T}.Update" />
    public async Task Update(
        AuthorEntity entity)
    {
        await using var context = _factory.Create();
        
        await context.Authors
            .Where(a => a.Id == entity.Id)
            .ExecuteUpdateAsync(e => e
                .SetProperty(a => a.FirstName, entity.FirstName)
                .SetProperty(a => a.LastName, entity.LastName)
                .SetProperty(a => a.Email, entity.Email));
        await context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IRepository{T}.Delete" />
    public async Task Delete(
        AuthorEntity authorEntity)
    {
        await using var context = _factory.Create();
        var author = await context.Authors.FindAsync(authorEntity.Id);

        if (author != null)
        {
            context.Authors.Remove(author);
            await context.SaveChangesAsync();
        }
    }
}