namespace ApplicationCore;

public class OrdersHistoryQueryParams
{
    public int Restaurant { get; set; } = -1;
    public string Status { get; set; } = "all";
    public string Search { get; set; } = string.Empty;
}
