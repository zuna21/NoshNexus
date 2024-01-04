using ApplicationCore.Entities;

namespace ApplicationCore;

public class RestaurantBlockedCustomers
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public int CustomerId { get; set; }


    // Navigation properties
    public Restaurant Restaurant { get; set; }
    public Customer Customer { get; set; }
}
