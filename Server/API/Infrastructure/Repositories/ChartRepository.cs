using ApplicationCore;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Entities;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

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

    public async Task<OwnerDtos.LineChartDto> GetOrdersByHour(int restaurantId, int ownerId, OrdersByHourQueryParams ordersByHourQueryParams)
    {
        var query = _context.Orders
            .Where(x => x.RestaurantId == restaurantId && x.Restaurant.OwnerId == ownerId);

        query = query.Where(x => x.CreatedAt == DateTime.Parse(ordersByHourQueryParams.Date));

        List<string> labels = [];
        List<int> data = [];
        for (int i = ordersByHourQueryParams.StartTime; i <= ordersByHourQueryParams.EndTime; i++)
        {
            var j = i == 24 ? 0 : i;
            var time = $"{j}:00:00";
            var hourBefore = j == 0 ? 23 : j-1;
            query = query
                .Where(x => x.CreatedAt > DateTime.Parse($"{ordersByHourQueryParams.Date} {hourBefore}:00:00") && x.CreatedAt <= DateTime.Parse($"{ordersByHourQueryParams.Date} {time}"));
            labels.Add(time);
            data.Add(await query.CountAsync());
        }

        return new OwnerDtos.LineChartDto()
        {
            Data = data,
            Labels = labels
        };
    }

    public async Task<OwnerDtos.PieChartDto> GetTopTenMenuItems(int restaurantId, int ownerId, TopTenMenuOrdersQueryParams topTenMenuOrdersQueryParams)
    {
        var query = _context.MenuItems
            .Where(x => x.IsDeleted == false && x.Menu.RestaurantId == restaurantId && x.Menu.Restaurant.OwnerId == ownerId);

        if (topTenMenuOrdersQueryParams.Menu != -1)
            query = query.Where(x => x.MenuId == topTenMenuOrdersQueryParams.Menu);

        query = query
            .OrderByDescending(x => x.OrderCount)
            .Take(10);

        return new OwnerDtos.PieChartDto()
        {
            Data = await query.Select(x => x.OrderCount).ToListAsync(),
            Labels = await query.Select(x => x.Name).ToListAsync()
        };
    }
}
