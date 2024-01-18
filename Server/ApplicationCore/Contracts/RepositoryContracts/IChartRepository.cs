using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace ApplicationCore;

public interface IChartRepository
{
    Task<ICollection<int>> GetOrdersByDay(int restaurantId, int ownerId, OrdersByDayQueryParams ordersByDayQueryParams);
    Task<OwnerDtos.PieChartDto> GetTopTenMenuItems(int restaurantId, int ownerId, TopTenMenuOrdersQueryParams topTenMenuOrdersQueryParams);
    Task<OwnerDtos.LineChartDto> GetOrdersByHour(int restaurantId, int ownerId, OrdersByHourQueryParams ordersByHourQueryParams);
}
