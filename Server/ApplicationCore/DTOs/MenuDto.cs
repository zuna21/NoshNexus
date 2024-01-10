namespace ApplicationCore.DTOs;

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

public class EmployeeEditMenuDto
{
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
    public PagedList<MenuItemCardDto> MenuItems { get; set; }
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

public class GetEmployeeMenuEditDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}