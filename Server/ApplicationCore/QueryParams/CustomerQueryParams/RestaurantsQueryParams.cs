namespace ApplicationCore.QueryParams.CustomerQueryParams;

public class RestaurantsQueryParams
{
    public int PageSize { get; set; } = 10;
    public int PageIndex { get; set; } = 0;
    public double? Latitude { get; set; } = null;
    public double? Longitude { get; set; } = null;
}
