namespace ReportService.Data.Entities;

/// <summary>
/// Represents an author entity with properties for identifying and describing an author.
/// </summary>
public sealed class AuthorEntity
{
    public long Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public ICollection<ReportEntity> Reports { get; set; }
}