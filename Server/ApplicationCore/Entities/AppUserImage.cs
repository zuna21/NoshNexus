namespace ApplicationCore.Entities;

public class AppUserImage
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public string Name { get; set; }
    public string UniqueName { get; set; }
    public long Size { get; set; }
    public string ContentType { get; set; }
    public string ContainerName { get; set; }
    public string Url { get; set; }
    public AppUserImageType Type { get; set; } = AppUserImageType.Gallery;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    // navigation properties
    public AppUser AppUser { get; set; }
}

public enum AppUserImageType 
{
    Profile,
    Gallery
}