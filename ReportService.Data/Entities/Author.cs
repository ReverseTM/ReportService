namespace ReportService.Data.Entities;

public class Author
{
    public long Id { get; set; }
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public string Email { get; set; }
    
    public ICollection<Report> Reports { get; set; }
}