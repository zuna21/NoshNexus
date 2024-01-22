namespace ApplicationCore.Entities;

public class Customer
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public int? CountryId { get; set; }
    public string UniqueUsername { get; set; }
    public bool IsActivated { get; set; } = false;
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public string City { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;




    // Navigation properties
    public AppUser AppUser { get; set; }
    public List<Order> Orders { get; set; } = [];
    public Country Country { get; set; }
    public List<RestaurantBlockedCustomers> BlockedRestaurants { get; set; } = [];
    public List<FavouriteCustomerRestaurant> FavouriteRestaurants { get; set; } = [];
    public List<FavouriteCustomerMenuItem> FavouriteMenuItems { get; set; } = [];
    public List<RestaurantReview> Reviews { get; set; } = [];
}
