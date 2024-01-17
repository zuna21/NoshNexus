﻿namespace ApplicationCore.QueryParams.CustomerQueryParams;

public class MenusQueryParams
{
    public int PageSize { get; set; } = 10;
    public int PageIndex { get; set; } = 0;
    public string Search { get; set; } = string.Empty;
}
