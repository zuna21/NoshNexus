namespace ApplicationCore.Entities;

public class RestaurantImage
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string UniqueName { get; set; }
    public long Size { get; set; }
    public string ContentType { get; set; }
    public string ContainerName { get; set; }
    public string Url { get; set; }
    public RestaurantImageType Type { get; set; } = RestaurantImageType.Gallery;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    // navigation properties
    public Restaurant Restaurant { get; set; }
}


public enum RestaurantImageType 
{
    Profile,
    Gallery
}
