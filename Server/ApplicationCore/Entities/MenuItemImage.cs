namespace ApplicationCore.Entities;

public class MenuItemImage
{
    public int Id { get; set; }
    public int MenuItemId { get; set; }
    public string Name { get; set; }
    public string UniqueName { get; set; }
    public long Size { get; set; }
    public string ContentType { get; set; }
    public string FullPath { get; set; }
    public string RelativePath { get; set; }
    public string Url { get; set; }
    public MenuItemImageType Type { get; set; } = MenuItemImageType.Gallery;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    // navigation properties
    public MenuItem MenuItem { get; set; }
}


public enum MenuItemImageType 
{
    Profile,
    Gallery
}