using System.ComponentModel.DataAnnotations;

namespace API;

public class Owner
{
    public int Id { get; set; }
    [Required]
    public int AppUserId { get; set; }
    public int CountryId { get; set; }
    [Required]
    public string UniqueUsername { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime Birth { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    // Navigation Properties
    public AppUser AppUser { get; set; }
    public Country Country  { get; set; }
    public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    public List<OwnerImage> OwnerImages { get; set; } = new List<OwnerImage>();
}
