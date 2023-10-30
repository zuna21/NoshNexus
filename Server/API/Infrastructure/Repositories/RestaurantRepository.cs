
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
            .Where(x => x.Owner.Id == owner.Id)
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

    public async Task<RestaurantDetailsDto> GetRestaurantDetails(int restaurantId, Owner owner)
    {
        return await _context.Restaurants
            .Where(x => x.OwnerId == owner.Id)
            .Select(r => new RestaurantDetailsDto
            {
                Address = r.Address,
                City = r.City,
                Country = r.Country.Name,
                Description = r.Description,
                FacebookUrl = r.FacebookUrl,
                InstagramUrl = r.InstagramUrl,
                WebsiteUrl = r.WebsiteUrl,
                IsActive = r.IsActive,
                PostalCode = r.PostalCode,
                Name = r.Name,
                EmployeesNumber = 0,
                Id = r.Id,
                MenusNumber = 0,
                PhoneNumber = r.PhoneNumber,
                RestaurantImages = new List<string>(),
                TodayOrdersNumber = 0,
            }).FirstOrDefaultAsync(x => x.Id == restaurantId);
    }

    public async Task<ICollection<RestaurantSelectDto>> GetRestaurantSelect(Owner owner)
    {
        return await _context.Restaurants
            .Where(x => x.OwnerId == owner.Id)
            .Select(r => new RestaurantSelectDto
            {
                Id = r.Id,
                Name = r.Name
            })
            .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
