using ApplicationCore.DTOs;

namespace ApplicationCore;

public interface IChartService
{
    Task<Response<ICollection<WeekDayOrdersDto>>> GetWeekDayOrders(int restaurantId);
    Task<Response<ICollection<TopTenMenuItemsDto>>> GetTopTenMenuItems(int restaurantId);
}
