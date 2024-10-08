﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportService.Data.Entities;

namespace ReportService.Data.Configuration;

/// <summary>
/// Configures the <see cref="AuthorEntity"/> entity in the Entity Framework model.
/// </summary>
public sealed class AuthorConfiguration : IEntityTypeConfiguration<AuthorEntity>
{
    /// <inheritdoc cref="Microsoft.EntityFrameworkCore.IEntityTypeConfiguration{T}.Configure" />
    public void Configure(
        EntityTypeBuilder<AuthorEntity> builder)
    {
        builder
            .ToTable("authors")
            .HasKey(a => a.Id);
        
        builder
            .HasMany(a => a.Reports)
            .WithOne(r => r.Author)
            .HasForeignKey(r => r.AuthorId);
        
        builder.Property(a => a.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        
        builder.Property(a => a.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(a => a.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(20)
            .IsRequired();
        
        builder.Property(a => a.Email)
            .HasColumnName("email")
            .HasMaxLength(30)
            .IsRequired();

        builder.HasIndex(a => a.Email).IsUnique();
    }
}