using ApplicationCore.DTOs;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace ApplicationCore;

public interface IChartService
{
    Task<Response<ICollection<int>>> GetOrdersByDay(int restaurantId, OrdersByDayQueryParams ordersByDayQueryParams);
    Task<Response<OwnerDtos.PieChartDto>> GetTopTenMenuItems(int restaurantId, TopTenMenuOrdersQueryParams topTenMenuOrdersQueryParams);
    Task<Response<OwnerDtos.LineChartDto>> GetOrdersByHour(int restaurantId, OrdersByHourQueryParams ordersByHourQueryParams);
}
