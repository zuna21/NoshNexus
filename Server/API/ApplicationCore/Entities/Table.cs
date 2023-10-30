namespace API;

public class Table
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public Restaurant Restaurant { get; set; }
}
