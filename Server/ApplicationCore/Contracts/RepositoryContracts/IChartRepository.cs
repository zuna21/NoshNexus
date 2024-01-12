namespace ApplicationCore;

public interface IChartRepository
{
    Task<ICollection<int>> GetOrdersByDay(int restaurantId, int ownerId, OrdersByDayQueryParams ordersByDayQueryParams);
    Task<PieChartDto> GetTopTenMenuItems(int restaurantId, int ownerId, TopTenMenuOrdersQueryParams topTenMenuOrdersQueryParams);
}
