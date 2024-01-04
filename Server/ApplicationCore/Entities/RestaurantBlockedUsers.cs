using ApplicationCore.Entities;

namespace ApplicationCore;

public class RestaurantBlockedUsers
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public int AppUserId { get; set; }



    // Navigation properties
    public Restaurant Restaurant { get; set; }
    public AppUser AppUser { get; set; }
}
