
using Microsoft.EntityFrameworkCore;

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

    public async Task<ICollection<RestaurantCardDto>> GetOwnerRestaurants(Owner owner)
    {
        return await _context.Restaurants
            .Where(x => x.Owner == owner)
            .Select(r => new RestaurantCardDto
            {
                Id = r.Id,
                Address = r.Address,
                City = r.City,
                Country = r.Country.Name,
                IsOpen = r.IsOpen,
                Name = r.Name,
                ProfileImage = null
            }).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
