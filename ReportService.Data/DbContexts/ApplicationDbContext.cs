﻿using Microsoft.EntityFrameworkCore;
using ReportService.Data.Configuration;
using ReportService.Data.Entities;

namespace ReportService.Data.DbContexts;

public class ApplicationDbContext : DbContext
{
    public DbSet<ReportEntity> Reports { get; set; }
    
    public DbSet<AuthorEntity> Authors { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new ReportConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}