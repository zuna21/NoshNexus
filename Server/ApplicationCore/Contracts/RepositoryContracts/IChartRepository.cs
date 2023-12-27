namespace ApplicationCore;

public interface IChartRepository
{
    Task<ICollection<WeekDayOrdersDto>> GetWeekDayOrders(int restaurantId);
    Task<ICollection<TopTenMenuItemsDto>> GetTopTenMenuItems(int restaurantId);
}
