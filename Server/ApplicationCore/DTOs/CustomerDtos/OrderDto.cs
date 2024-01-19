namespace ApplicationCore.DTOs.CustomerDtos;

public class OrderDto
{

}

public class CreateOrderDto
{
    public int TableId { get; set; }
    public string Note { get; set; }
    public ICollection<int> MenuItemIds { get; set; }
}