namespace ApplicationCore.Entities;

public class OwnerImage
{
    public int Id { get; set; }
    public int OwnerId { get; set; }
    public string Name { get; set; }
    public string UniqueName { get; set; }
    public long Size { get; set; }
    public string ContentType { get; set; }
    public string FullPath { get; set; }
    public string RelativePath { get; set; }
    public string Url { get; set; }
    public OwnerImageType Type { get; set; } = OwnerImageType.Gallery;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    // navigation properties
    public Owner Owner { get; set; }
}

public enum OwnerImageType 
{
    Profile,
    Gallery
}
