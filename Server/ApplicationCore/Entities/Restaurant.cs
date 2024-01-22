using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Restaurant
{
    public int Id { get; set; }
    [Required]
    public int OwnerId { get; set; }
    [Required]
    public int CountryId { get; set; }
    [Required]
    public int CurrencyId { get; set; }
    [Required]
    public string Name { get; set; }
    public int PostalCode { get; set; } = 0;
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string WebsiteUrl { get; set; }
    public double Latitude { get; set; } = 0;
    public double Longitude { get; set; } = 0;
    public bool IsActive { get; set; } = false;
    public bool IsOpen { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    // Navigation properties
    public Owner Owner { get; set; }
    public Country Country { get; set; }
    public Currency Currency { get; set; }
    public List<Table> Tables { get; set; } = [];
    public List<Menu> Menus { get; set; } = [];
    public List<Employee> Employees { get; set; } = [];
    public List<RestaurantImage> RestaurantImages { get; set; } = [];
    public List<Order> Orders { get; set; } = [];
    public List<RestaurantBlockedCustomers> BlockedCustomers { get; set; } = [];
    public List<FavouriteCustomerRestaurant> FavouriteCustomers { get; set; } = [];
    public List<RestaurantReview> Reviews { get; set; } = [];
}