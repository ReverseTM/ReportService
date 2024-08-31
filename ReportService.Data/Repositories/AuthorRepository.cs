using Microsoft.EntityFrameworkCore;
using ReportService.Data.DbContexts;
using ReportService.Data.Entities;
using ReportService.Data.Interfaces;

namespace ReportService.Data.Repositories;

public class AuthorRepository : IRepository<Author>
{
    private readonly ApplicationDbContext _context;

    public AuthorRepository(ApplicationDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }
    
    public async Task<Author?> GetById(long id)
    {
        return await _context.Authors
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<IEnumerable<Author>> GetAll()
    {
        return await _context.Authors
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task Add(Author entity)
    {
        await _context.Authors.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Author entity)
    {
        _context.Authors.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(long id)
    {
        var author = await _context.Authors.FindAsync(id);

        if (author != null)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}