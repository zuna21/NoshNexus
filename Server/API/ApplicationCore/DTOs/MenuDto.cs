namespace API;

public class MenuDto
{

}

public class CreateMenuDto
{
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}

public class MenuCardDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsActive { get; set; }
    public string Description { get; set; }
    public int MenuItemNumber { get; set; }
    public string RestaurantName { get; set; }
}

public class MenuDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string RestaurantImage { get; set; }
    public ICollection<MenuItemCardDto> MenuItems { get; set; }
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

public class GetMenuEditDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public int RestaurantId { get; set; }
    public ICollection<RestaurantSelectDto> OwnerRestaurants { get; set; }
}