using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API;

public class Owner
{
    public int Id { get; set; }
    [Required]
    public string IdentityUserId { get; set; }
    [Required]
    public string UniqueUsername { get; set; }
    public bool IsActive { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    // Navigation Properties
    public IdentityUser IdentityUser { get; set; }
    public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}
