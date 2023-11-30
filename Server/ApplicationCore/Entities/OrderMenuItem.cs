namespace ApplicationCore.Entities;

public class OrderMenuItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public int MenuItemId  {get; set; }

    // Navigation properties
    public Order Order { get; set; }
    public MenuItem MenuItem { get; set; }
}
