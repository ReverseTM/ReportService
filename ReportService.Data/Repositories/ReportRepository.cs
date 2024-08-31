using Microsoft.EntityFrameworkCore;
using ReportService.Data.DbContexts;
using ReportService.Data.Entities;
using ReportService.Data.Interfaces;

namespace ReportService.Data.Repositories;

public class ReportRepository : IRepository<Report>
{
    private readonly ApplicationDbContext _context;

    public ReportRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<Report?> GetById(long id)
    { 
        return await _context.Reports
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Report>> GetAll()
    {
        return await _context.Reports
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task Add(Report entity)
    {
        await _context.Reports.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Report entity)
    {
        _context.Reports.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        var report = await _context.Reports.FindAsync(id);

        if (report != null)
        {
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
        }
    }
}