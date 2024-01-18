using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace ApplicationCore;

public interface IChartRepository
{
    Task<ICollection<int>> GetOrdersByDay(int restaurantId, int ownerId, OwnerQueryParams.OrdersByDayQueryParams ordersByDayQueryParams);
    Task<OwnerDtos.PieChartDto> GetTopTenMenuItems(int restaurantId, int ownerId, OwnerQueryParams.TopTenMenuOrdersQueryParams topTenMenuOrdersQueryParams);
    Task<OwnerDtos.LineChartDto> GetOrdersByHour(int restaurantId, int ownerId, OwnerQueryParams.OrdersByHourQueryParams ordersByHourQueryParams);
}
