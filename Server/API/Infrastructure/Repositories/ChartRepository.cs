using ApplicationCore;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;

namespace API;

public class ChartRepository(
    DataContext dataContext
) : IChartRepository
{
    private readonly DataContext _context = dataContext;

    public async Task<ICollection<int>> GetOrdersByDay(int restaurantId, int ownerId, OrdersByDayQueryParams ordersByDayQueryParams)
    {

        var query = _context.Orders.Where(x => x.RestaurantId == restaurantId && x.Restaurant.OwnerId == ownerId);

        query = query.Where(x => x.CreatedAt >= DateTime.Parse(ordersByDayQueryParams.StartDate));
        query = query.Where(x => x.CreatedAt <= DateTime.Parse(ordersByDayQueryParams.EndDate));

        if (string.Equals(ordersByDayQueryParams.Status.ToLower(), "accepted"))
            query = query.Where(x => x.Status == OrderStatus.Accepted);
        
        if (string.Equals(ordersByDayQueryParams.Status.ToLower(), "declined"))
            query = query.Where(x => x.Status == OrderStatus.Declined);


        var data = await query
            .GroupBy(x => x.CreatedAt.DayOfWeek)
            .Select(g => new { DayOfWeek = g.Key, OrderCount = g.Count() })
            .OrderBy(x => x.DayOfWeek)
            .ToListAsync();

        // Initialize an array with counts for each day of the week
        int[] resultArray = new int[7];

        // Populate the result array based on the grouped data
        foreach (var item in data)
        {
            // DayOfWeek enumeration starts with Sunday as 0, Monday as 1, and so on
            int dayIndex = (int)item.DayOfWeek == 0 ? 6 : (int)item.DayOfWeek - 1;
            resultArray[dayIndex] = item.OrderCount;
        }

        return resultArray;

    }
}
