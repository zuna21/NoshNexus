﻿namespace ApplicationCore.QueryParams.CustomerQueryParams;

public class MenuItemsQueryParams
{  
    public int PageSize { get; set; } = 10;
    public int PageIndex { get; set; } = 0;
    public string Search { get; set; } = string.Empty;
}
