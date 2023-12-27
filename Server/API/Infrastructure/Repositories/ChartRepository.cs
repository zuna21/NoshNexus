using System.Globalization;
using ApplicationCore;
using Microsoft.EntityFrameworkCore;

namespace API;

public class ChartRepository(
    DataContext dataContext
) : IChartRepository
{
    private readonly DataContext _context = dataContext;

    public async Task<ICollection<TopTenMenuItemsDto>> GetTopTenMenuItems(int restaurantId)
    {
        return await _context.MenuItems
            .Where(x => x.Menu.RestaurantId == restaurantId)
            .OrderByDescending(x => x.OrderCount)
            .Take(10)
            .Select(x => new TopTenMenuItemsDto
            {
                Name = x.Name,
                Value = x.OrderCount
            })
            .ToListAsync();
    }

    public async Task<ICollection<WeekDayOrdersDto>> GetWeekDayOrders(int restaurantId)
    {
        return await _context.Orders
            .Where(x => x.RestaurantId == restaurantId)
            .GroupBy(x => x.CreatedAt.DayOfWeek)
            .OrderBy(x => x.Key)
            .Select(x => new WeekDayOrdersDto
            {
                Name = x.Key.ToString(),
                Value = x.Count()
            })
            .ToListAsync();
    }
}
