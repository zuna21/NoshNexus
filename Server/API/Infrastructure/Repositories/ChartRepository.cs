using ApplicationCore;
using Microsoft.EntityFrameworkCore;

namespace API;

public class ChartRepository(
    DataContext dataContext
) : IChartRepository
{
    private readonly DataContext _context = dataContext;

    public async Task<ICollection<WeekDayOrdersDto>> GetWeekDayOrders(int restaurantId)
    {
        return await _context.Orders
            .Where(x => x.RestaurantId == restaurantId)
            .GroupBy(x => x.CreatedAt.DayOfWeek)
            .Select(x => new WeekDayOrdersDto
            {
                Name = x.Key.ToString(),
                Value = x.Count()
            })
            .ToListAsync();
    }
}
