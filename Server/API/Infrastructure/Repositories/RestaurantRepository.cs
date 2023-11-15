
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

    public async Task<Restaurant> GetOwnerRestaurant(int restaurantId, int ownerId)
    {
        return await _context.Restaurants.FirstOrDefaultAsync(
            x => x.OwnerId == ownerId && x.Id == restaurantId
        );
    }

    public async Task<ICollection<RestaurantCardDto>> GetRestaurants(int ownerId)
    {
        return await _context.Restaurants
            .Where(x => x.Owner.Id == ownerId)
            .Select(r => new RestaurantCardDto
            {
                Id = r.Id,
                Address = r.Address,
                City = r.City,
                Country = r.Country.Name,
                IsOpen = r.IsOpen,
                Name = r.Name,
                ProfileImage = r.RestaurantImages
                    .Where(x => x.IsDeleted == false && x.Type == RestaurantImageType.Profile)
                    .Select(x => x.Url)
                    .FirstOrDefault()
            }).ToListAsync();
    }

    public async Task<RestaurantDetailsDto> GetRestaurant(int restaurantId, int ownerId)
    {
        return await _context.Restaurants
            .Where(x => x.OwnerId == ownerId && x.Id == restaurantId)
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
                RestaurantImages = r.RestaurantImages
                    .Where(x => x.IsDeleted == false)
                    .Select(x => x.Url)
                    .ToList(),
                TodayOrdersNumber = 0,
            }).FirstOrDefaultAsync();
    }

    public async Task<GetRestaurantEditDto> GetRestaurantEdit(int restaurantId, int ownerId)
    {
        return await _context.Restaurants
            .Where(x => x.OwnerId == ownerId && x.Id == restaurantId)
            .Select(r => new GetRestaurantEditDto
            {
                Address = r.Address,
                City = r.City,
                CountryId = r.Country.Id,
                CurrencyId = r.Currency.Id,
                Description = r.Description,
                FacebookUrl = r.FacebookUrl,
                InstagramUrl = r.InstagramUrl,
                WebsiteUrl = r.WebsiteUrl,
                Id = r.Id,
                IsActive = r.IsActive,
                ProfileImage = r.RestaurantImages
                    .Where(x => x.Type == RestaurantImageType.Profile && x.IsDeleted == false)
                    .Select(n => new ImageDto
                    {
                        Id = n.Id,
                        Size = n.Size,
                        Url = n.Url
                    })
                    .FirstOrDefault(),
                Images = r.RestaurantImages
                    .Where(x => x.Type == RestaurantImageType.Gallery && x.IsDeleted == false)
                    .Select(x => new ImageDto
                    {
                        Id = x.Id,
                        Size = x.Size,
                        Url = x.Url
                    })
                    .ToList(),
                Name = r.Name,
                PhoneNumber = r.PhoneNumber,
                PostalCode = r.PostalCode
            }).FirstOrDefaultAsync();
    }

    public async Task<ICollection<RestaurantSelectDto>> GetRestaurantSelect(int ownerId)
    {
        return await _context.Restaurants
            .Where(x => x.OwnerId == ownerId)
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
