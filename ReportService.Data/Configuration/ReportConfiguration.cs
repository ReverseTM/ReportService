using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReportService.Data.Entities;

namespace ReportService.Data.Configuration;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder
            .ToTable("reports")
            .HasKey(r => r.Id);

        builder
            .HasOne(r => r.Author)
            .WithMany(a => a.Reports)
            .HasForeignKey(r => r.AuthorId);
        
        builder.Property(r => r.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();
        
        builder.Property(r => r.AuthorId)
            .HasColumnName("author_id")
            .IsRequired();
        
        builder.Property(r => r.WordFileUrl)
            .HasColumnName("word_file_url")
            .HasMaxLength(255)
            .IsRequired();
        
        builder.Property(r => r.RequestTime)
            .HasColumnName("request_time")
            .IsRequired();
        
        builder.Property(r => r.ReferencedFiles)
            .HasColumnName("referenced_files")
            .HasColumnType("text")
            .IsRequired();
        
        builder.HasIndex(r => r.WordFileUrl).IsUnique();
    }
}