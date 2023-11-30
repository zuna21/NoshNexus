namespace ApplicationCore.Entities;

public class EmployeeImage
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }
    public string Name { get; set; }
    public string UniqueName { get; set; }
    public long Size { get; set; }
    public string ContentType { get; set; }
    public string FullPath { get; set; }
    public string RelativePath { get; set; }
    public string Url { get; set; }
    public EmployeeImageType Type { get; set; } = EmployeeImageType.Gallery;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    // navigation properties
    public Employee Employee { get; set; }
}


public enum EmployeeImageType 
{
    Profile,
    Gallery
}