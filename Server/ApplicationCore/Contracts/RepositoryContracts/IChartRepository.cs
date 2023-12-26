namespace ApplicationCore;

public interface IChartRepository
{
    Task<ICollection<WeekDayOrdersDto>> GetWeekDayOrders(int restaurantId);
}
