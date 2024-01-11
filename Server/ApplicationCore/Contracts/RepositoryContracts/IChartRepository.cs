namespace ApplicationCore;

public interface IChartRepository
{
    Task<ICollection<int>> GetOrdersByDay(int restaurantId, int ownerId);
}
