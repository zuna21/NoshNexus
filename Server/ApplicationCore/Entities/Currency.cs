using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Currency
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }



    // navigation properties
    public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}

