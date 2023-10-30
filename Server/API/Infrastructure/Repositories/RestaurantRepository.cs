
namespace API;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly DataContext _context;
    public RestaurantRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void Create(Restaurant restaurant)
    {
        _context.Restaurants.Add(restaurant);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
