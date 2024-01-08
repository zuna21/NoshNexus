namespace ApplicationCore;

public class OrdersHistoryQueryParams
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public int Restaurant { get; set; } = -1;
    public string Status { get; set; } = "all";
    public string Search { get; set; } = string.Empty;
}
