using ApplicationCore.DTOs;

namespace ApplicationCore;

public interface IChartService
{
    Task<Response<ICollection<int>>> GetOrdersByDay(int restaurantId);
}
