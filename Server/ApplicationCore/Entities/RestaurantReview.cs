using System.ComponentModel.DataAnnotations;
using ApplicationCore.Entities;

namespace ApplicationCore;

public class RestaurantReview
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public int CustomerId { get; set; }
    public float Rating { get ; set; } = 5;
    [Required]
    public string Review { get ; set; }
    public DateTime CreatedAt { get; set; }

    


    // Navigation Properites
    public Restaurant Restaurant { get; set; }
    public Customer Customer { get; set; }
}
