namespace API;

public class Customer
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public string UniqueUsername { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public AppUser AppUser { get; set; }
}
