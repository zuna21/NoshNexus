namespace ApplicationCore;

public interface IChartRepository
{
    Task<ICollection<VerticalBarChartDto>> GetOrdersByDay(int restaurantId, int ownerId);
}
