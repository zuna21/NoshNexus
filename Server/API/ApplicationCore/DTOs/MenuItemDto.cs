namespace API;

public class MenuItemDto
{

}

public class CreateMenuItemDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public bool IsActive { get; set; }
    public bool HasSpecialOffer { get; set; }
    public double SpecialOfferPrice { get; set; }
}

public class MenuItemCardDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string Image { get; set; }
    public bool IsActive { get; set; }
    public bool HasSpecialOffer { get; set; }
    public double SpecialOfferPrice { get; set; }

}