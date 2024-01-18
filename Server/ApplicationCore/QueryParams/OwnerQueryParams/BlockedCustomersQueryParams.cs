namespace ApplicationCore.QueryParams.OwnerQueryParams;

public class BlockedCustomersQueryParams
{
    public int PageSize { get; set; } = 10;
    public int PageIndex { get; set; } = 0;
    public int Restaurant { get; set; } = -1;
    public string Search { get; set; } = string.Empty;
}
