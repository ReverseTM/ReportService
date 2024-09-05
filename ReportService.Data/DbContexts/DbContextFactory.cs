using Microsoft.EntityFrameworkCore;

namespace ReportService.Data.DbContexts;

/// <summary>
/// A factory class for creating instances of <see cref="ApplicationDbContext"/>.
/// </summary>
public sealed class DbContextFactory
{
    private readonly DbContextOptions<ApplicationDbContext> _options;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="DbContextFactory"/> class with the specified options.
    /// Throws an <see cref="ArgumentNullException"/> if the provided options are null.
    /// </summary>
    /// <param name="options">The options to configure the <see cref="ApplicationDbContext"/>.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="options"/> is null.</exception>
    public DbContextFactory(
        DbContextOptions<ApplicationDbContext> options)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
    }
    
    /// <summary>
    /// Creates and returns a new instance of <see cref="ApplicationDbContext"/> using the provided options.
    /// </summary>
    /// <returns>A new instance of <see cref="ApplicationDbContext"/> configured with the specified options.</returns>
    public ApplicationDbContext Create()
    {
        return new ApplicationDbContext(_options);
    }
}