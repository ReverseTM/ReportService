using Microsoft.EntityFrameworkCore;
using ReportService.Data.Configuration;
using ReportService.Data.Entities;

namespace ReportService.Data.DbContexts;

/// <summary>
/// Represents the application's database context, providing access to the database tables through <see cref="DbSet{T}"/> properties.
/// </summary>
public sealed class ApplicationDbContext : DbContext
{
    /// <summary>
    /// Gets or sets the collection of <see cref="ReportEntity"/> entities in the database.
    /// </summary>
    public DbSet<ReportEntity> Reports { get; set; }
    
    /// <summary>
    /// Gets or sets the collection of <see cref="AuthorEntity"/> entities in the database.
    /// </summary>
    public DbSet<AuthorEntity> Authors { get; set; }
    
    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class with the specified options.
    /// </summary>
    /// <param name="options">The options to configure the <see cref="ApplicationDbContext"/>.</param>
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options) : base(options) { }

    /// <inheritdoc cref="Microsoft.EntityFrameworkCore.DbContext.OnModelCreating" />
    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new ReportConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}