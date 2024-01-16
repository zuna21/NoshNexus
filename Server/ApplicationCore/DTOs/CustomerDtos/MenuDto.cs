namespace ApplicationCore.DTOs.CustomerDtos;

public class MenuDto
{
    public int Id { get; set; }
    public MenuRestaurant Restaurant { get; set; }
    public int TotalMenuItems { get; set; }
    public string Description { get; set; }
}

public class MenuCardDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int MenuItemNumber { get; set; }
    public string RestaurantName { get; set; }
}

public class MenuRestaurant 
{
    public int Id { get; set; }
    public string Name { get; set; }
}