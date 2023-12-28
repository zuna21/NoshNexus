namespace ApplicationCore;

public class MenusQueryParams
{
    public int PageIndex { get; set; } = 0;
    public int PageSize { get; set; } = 10;
    public string Search { get; set; } = string.Empty;
    public string Activity { get; set; } = "all";
    public int Restaurant { get; set; } = -1;
}
