namespace ApplicationCore;

public class TablesQueryParams
{
    public int MaxPageSize { get; set; } = 50;
    public int DefaultPageSize { get; set; } = 25;

    public int PageIndex { get; set; } = 0;
    public int PageSize 
    {
        get => DefaultPageSize;
        set => DefaultPageSize = (value > MaxPageSize) ? MaxPageSize : value;
    }
}
