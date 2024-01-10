using ApplicationCore.DTOs;

namespace ApplicationCore;

public interface IChartService
{
    Task<Response<ICollection<VerticalBarChartDto>>> GetOrdersByDay(int restaurantId);
}
