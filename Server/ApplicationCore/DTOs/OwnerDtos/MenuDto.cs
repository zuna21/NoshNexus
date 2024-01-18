namespace ApplicationCore.DTOs.OwnerDtos;

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

public class EditMenuDto 
{
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public int RestaurantId { get; set; }
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

public class GetMenuEditDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public int RestaurantId { get; set; }
    public ICollection<GetRestaurantForSelectDto> OwnerRestaurants { get; set; }
}

public class GetRestaurantMenusForSelectDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class GetMenuDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string RestaurantImage { get; set; }
    public PagedList<MenuItemCardDto> MenuItems { get; set; }
}