using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class MenuItem
{
    public int Id { get; set; }
    public int MenuId  { get; set; }
    [Required]
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; } = 0;
    public bool IsActive { get; set; } = false;
    public bool HasSpecialOffer { get; set; } = false;
    public long OrderCount { get; set; } = 0;
    public double SpecialOfferPrice { get; set; } = 0;
    public bool IsDeleted { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    


    // Navigation properties
    public Menu Menu { get; set; }
    public List<MenuItemImage> MenuItemImages { get; set; } = [];
    public List<OrderMenuItem> OrderMenuItems { get; set; } = [];
    public List<FavouriteCustomerMenuItem> FavouriteCustomers { get; set; } = [];
}
