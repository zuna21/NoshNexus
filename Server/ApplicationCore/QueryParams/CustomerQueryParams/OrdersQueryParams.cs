namespace ApplicationCore.QueryParams.CustomerQueryParams;

public class OrdersQueryParams
{
    public int PageSize { get; set; } = 10;
    public int PageIndex { get; set; } = 0;
    public string Status { get; set; } = "all";
    public string Search { get; set; } = string.Empty;
}
