namespace API;

public class OrderDto
{

}

public class OrderCardDto
{
    public int Id { get; set; }
    public OrderCardUserDto User { get; set; }
    public OrderRestaurantDto Restaurant  { get; set; }
    public string TableName { get; set; }
    public string Note { get; set; }
    public double TotalPrice { get; set; }
    public int TotalItems { get; set; }
    public string Status { get; set; }
    public string DeclineReason { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateOrderDto
{
    public int TableId { get; set; }
    public string Note { get; set; }
    public ICollection<int> MenuItemIds { get; set; }
}

public class OrderCardUserDto 
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string ProfileImage { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class OrderMenuItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
}

public class OrderRestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}