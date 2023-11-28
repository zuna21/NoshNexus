namespace API;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public int RestaurantId { get; set; }
    public int TableId { get; set; }

    public string Note { get; set; }
    public double TotalPrice { get; set; } = 0;
    public int TotalItems { get; set; } = 0;
    public string DeclineReason { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.InProgress;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    // Navigation properties
    public Customer Customer { get; set; }
    public Restaurant Restaurant { get; set; }
    public Table Table { get; set; }
    public List<OrderMenuItem> OrderMenuItems = new();
}


public enum OrderStatus 
{
    Accepted,
    Declined,
    InProgress
}