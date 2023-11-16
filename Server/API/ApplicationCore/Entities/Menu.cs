namespace API;

public class Menu
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    // Navigation properties
    public Restaurant Restaurant { get; set; }
    public List<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}
