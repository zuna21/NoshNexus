using ApplicationCore.DTOs;

namespace ApplicationCore;

public interface IChartService
{
    Task<Response<ICollection<int>>> GetOrdersByDay(int restaurantId, OrdersByDayQueryParams ordersByDayQueryParams);
    Task<Response<PieChartDto>> GetTopTenMenuItems(int restaurantId, TopTenMenuOrdersQueryParams topTenMenuOrdersQueryParams);
    Task<Response<LineChartDto>> GetOrdersByHour(int restaurantId, OrdersByHourQueryParams ordersByHourQueryParams);
}
