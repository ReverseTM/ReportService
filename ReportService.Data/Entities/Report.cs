namespace ReportService.Data.Entities;

public class ReportEntity
{
    public long Id { get; set; }
    
    public long AuthorId { get; set; }
    
    public DateTime RequestTime { get; set; }
    
    public string WordFileUrl { get; set; }
    
    public string ReferencedFiles { get; set; }
    
    public AuthorEntity Author { get; set; }
}