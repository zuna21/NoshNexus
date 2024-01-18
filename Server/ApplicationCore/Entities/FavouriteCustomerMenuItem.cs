using ApplicationCore.Entities;

namespace ApplicationCore;

public class FavouriteCustomerMenuItem
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int MenuItemId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    // Navigation properties
    public Customer Customer { get; set; }
    public MenuItem MenuItem { get; set; }
    
} 
