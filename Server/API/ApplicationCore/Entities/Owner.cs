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
    public bool IsActive { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    // Navigation Properties
    public AppUser AppUser { get; set; }
    public Country Country  { get; set; }
    public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}
