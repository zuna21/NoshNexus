using ApplicationCore.Entities;

namespace ApplicationCore;

public class FavouriteCustomerRestaurant
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int RestaurantId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Customer Customer { get; set; }
    public Restaurant Restaurant { get; set; }
}
