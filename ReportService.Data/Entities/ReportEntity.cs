namespace ReportService.Data.Entities;

/// <summary>
/// Represents a report entity with details about the report, including its associated author and metadata.
/// </summary>
public sealed class ReportEntity
{
    public long Id { get; set; }
    
    public long AuthorId { get; set; }
    
    public DateTime RequestTime { get; set; }
    
    public string WordFileUrl { get; set; }
    
    public string ReferencedFiles { get; set; }
    
    public AuthorEntity Author { get; set; }
}