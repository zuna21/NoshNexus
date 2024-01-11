using ApplicationCore;
using Microsoft.EntityFrameworkCore;

namespace API;

public class ChartRepository(
    DataContext dataContext
) : IChartRepository
{
    private readonly DataContext _context = dataContext;

    public async Task<ICollection<int>> GetOrdersByDay(int restaurantId, int ownerId)
    {

        var query = _context.Orders.Where(x => x.RestaurantId == restaurantId && x.Restaurant.OwnerId == ownerId);
        List<int> data = [];

        for (int i = 0; i < 7; i++) 
        {
            if (i < 6) 
            {
                query = query.Where(x => (int)x.CreatedAt.DayOfWeek == i + 1);
            }
            else 
            {
                query = query.Where(x => x.CreatedAt.DayOfWeek == 0);
            }

            data.Add(await query.CountAsync());
        }


        return data;
    }
}
