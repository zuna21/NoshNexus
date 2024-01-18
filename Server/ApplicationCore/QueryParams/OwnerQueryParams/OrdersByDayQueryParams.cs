namespace ApplicationCore.QueryParams.OwnerQueryParams;

public class OrdersByDayQueryParams
{
    public string StartDate { get; set; } = DateTime.UtcNow.AddDays(-7).ToString("dd-MM-yyyy");
    public string EndDate { get; set; } = DateTime.UtcNow.ToString("dd-MM-yyyy");
    public string Status { get; set; } = "all";
}
