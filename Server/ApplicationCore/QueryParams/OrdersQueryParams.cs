﻿namespace ApplicationCore;

public class OrdersQueryParams
{
    public int Restaurant { get; set; } = -1;
    public string Search { get; set; } = string.Empty;
}
