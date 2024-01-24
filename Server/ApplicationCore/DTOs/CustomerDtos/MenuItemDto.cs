namespace ApplicationCore.DTOs.CustomerDtos;

public class MenuItemDto
{

}

public class MenuItemCardDto 
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public MenuItemMenuDto Menu { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public bool HasSpecialOffer { get; set; }
    public bool IsFavourite { get; set; }
    public double SpecialOfferPrice { get; set; }
    public string ProfileImage { get; set; }
    public ICollection<string> Images { get; set; }
}

public class MenuItemMenuDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}
