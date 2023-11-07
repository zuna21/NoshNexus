namespace API;

public class RestaurantImage
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string UniqueName { get; set; }
    public int Size { get; set; }
    public string ContentType { get; set; }
    public string Path { get; set; }
    public RestaurantImageType Type { get; set; } = RestaurantImageType.Gallery;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    // navigation properties
    public Restaurant Restaurant { get; set; }
}


public enum RestaurantImageType 
{
    Profile,
    Gallery
}