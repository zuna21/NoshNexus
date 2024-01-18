namespace ApplicationCore.QueryParams.OwnerQueryParams;

public class OrdersByHourQueryParams
{
    public string Date { get; set; } = DateTime.UtcNow.ToString("dd-MM-yyyy");
    public int StartTime { get; set; } = 7;
    public int EndTime { get; set; } = 24;
}
