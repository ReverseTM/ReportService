using Microsoft.EntityFrameworkCore;
using ReportService.Data.DbContexts;
using ReportService.Data.Entities;
using ReportService.Data.Interfaces;

namespace ReportService.Data.Repositories;

/// <summary>
/// Repository for managing <see cref="ReportEntity"/> entities.
/// Provides methods for adding, updating, deleting, and retrieving authors from the database.
/// </summary>
public sealed class ReportRepository : IRepository<ReportEntity>
{
    private readonly DbContextFactory _factory;

    /// <summary>
    /// Initializes a new instance of the <see cref="ReportRepository"/> class with the specified <see cref="DbContextFactory"/>.
    /// </summary>
    /// <param name="factory">The factory used to create instances of the <see cref="ApplicationDbContext"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="factory"/> is null.</exception>
    public ReportRepository(
        DbContextFactory factory)
    {
        _factory = factory ?? throw new ArgumentNullException(nameof(factory));
    }
    
    /// <inheritdoc cref="IRepository{T}.Get" />
    public async Task<ReportEntity> Get(
        ReportEntity reportEntity)
    { 
        await using var context = _factory.Create();
        
        var report = await context.Reports
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Equals(reportEntity));

        if (report == null)
        {
            throw new KeyNotFoundException($"The report with id {reportEntity.Id} was not found.");
        }
        
        return report;
    }
    
    /// <inheritdoc cref="IRepository{T}.GetAll" />
    public async Task<IEnumerable<ReportEntity>> GetAll()
    {
        await using var context = _factory.Create();
        
        return await context.Reports
            .AsNoTracking()
            .ToListAsync();
    }

    /// <inheritdoc cref="IRepository{T}.Add" />
    public async Task Add(
        ReportEntity entity)
    {
        await using var context = _factory.Create();
        
        await context.Reports.AddAsync(entity);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IRepository{T}.AddRange" />
    public async Task AddRange(
        IEnumerable<ReportEntity> reportEntities)
    {
        await using var context = _factory.Create();
        
        await context.Reports.AddRangeAsync(reportEntities);
        await context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IRepository{T}.Update" />
    public async Task Update(
        ReportEntity reportEntity)
    {
        await using var context = _factory.Create();
        
        await context.Reports
            .Where(r => r.Id == reportEntity.Id)
            .ExecuteUpdateAsync(e => e
                .SetProperty(r => r.AuthorId, reportEntity.AuthorId)
                .SetProperty(r => r.RequestTime, reportEntity.RequestTime)
                .SetProperty(r => r.WordFileUrl, reportEntity.WordFileUrl)
                .SetProperty(r => r.ReferencedFiles, reportEntity.ReferencedFiles));
        await context.SaveChangesAsync();
    }

    /// <inheritdoc cref="IRepository{T}.Delete" />
    public async Task Delete(
        ReportEntity reportEntity)
    {
        await using var context = _factory.Create();
        var report = await context.Reports.FindAsync(reportEntity.Id);

        if (report != null)
        {
            context.Reports.Remove(report);
            await context.SaveChangesAsync();
        }
    }
}