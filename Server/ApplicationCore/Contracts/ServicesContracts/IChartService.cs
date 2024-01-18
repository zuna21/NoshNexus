using ApplicationCore.DTOs;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace ApplicationCore;

public interface IChartService
{
    Task<Response<ICollection<int>>> GetOrdersByDay(int restaurantId, OwnerQueryParams.OrdersByDayQueryParams ordersByDayQueryParams);
    Task<Response<OwnerDtos.PieChartDto>> GetTopTenMenuItems(int restaurantId, OwnerQueryParams.TopTenMenuOrdersQueryParams topTenMenuOrdersQueryParams);
    Task<Response<OwnerDtos.LineChartDto>> GetOrdersByHour(int restaurantId, OwnerQueryParams.OrdersByHourQueryParams ordersByHourQueryParams);
}
