using System.ComponentModel.DataAnnotations;

namespace API;

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
    public bool IsActive { get; set; } = false;
    public bool IsOpen { get; set; } = false;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;



    // Navigation properties
    public Owner Owner { get; set; }
    public Country Country { get; set; }
    public Currency Currency { get; set; }
    public List<Table> Tables { get; set; } = new List<Table>();
    public List<Menu> Menus { get; set; } = new List<Menu>();
    public List<Employee> Employees { get; set; } = new List<Employee>();
    public List<RestaurantImage> RestaurantImages { get; set; } = new List<RestaurantImage>();
}
