
using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

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

    public async Task<ICollection<RestaurantCardDto>> GetRestaurants(int ownerId, RestaurantsQueryParams restaurantsQueryParams)
    {
        var query = _context.Restaurants
            .Where(x => x.OwnerId == ownerId && x.IsDeleted == false);

        if (!string.IsNullOrEmpty(restaurantsQueryParams.Search))
            query = query.Where(x => x.Name.ToLower().Contains(restaurantsQueryParams.Search.ToLower()));

        return await query
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
            })
            .ToListAsync();
    }

    public async Task<RestaurantDetailsDto> GetRestaurant(int restaurantId, int ownerId)
    {
        return await _context.Restaurants
            .Where(x => x.OwnerId == ownerId && x.Id == restaurantId && x.IsDeleted == false)
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
                EmployeesNumber = r.Employees.Count,
                Id = r.Id,
                MenusNumber = r.Menus
                    .Where(x => x.IsDeleted == false)
                    .Count(),
                PhoneNumber = r.PhoneNumber,
                RestaurantImages = r.RestaurantImages
                    .Where(x => x.IsDeleted == false)
                    .Select(x => x.Url)
                    .ToList(),
                TodayOrdersNumber = r.Orders
                    .Where(o => o.CreatedAt.Day == DateTime.UtcNow.Day)
                    .Count(),
            }).FirstOrDefaultAsync();
    }

    public async Task<GetRestaurantEditDto> GetRestaurantEdit(int restaurantId, int ownerId)
    {
        return await _context.Restaurants
            .Where(x => x.OwnerId == ownerId && x.Id == restaurantId && x.IsDeleted == false)
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
            .Where(x => x.OwnerId == ownerId && x.IsDeleted == false)
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

    public async Task<Restaurant> GetAnyRestaurantById(int restaurantId)
    {
        return await _context.Restaurants
            .Where(x => x.Id == restaurantId)
            .FirstOrDefaultAsync();
    }

    public async Task<RestaurantDetailsDto> GetEmployeeRestaurantDetailsDto(int restaurantId)
    {
        return await _context.Restaurants
            .Where(x => x.Id == restaurantId)
            .Select(x => new RestaurantDetailsDto
            {
                Id = x.Id,
                Address = x.Address,
                City = x.City,
                Country = x.Country.Name,
                Description = x.Description,
                EmployeesNumber = x.Employees.Count,
                FacebookUrl = x.FacebookUrl,
                InstagramUrl = x.InstagramUrl,
                IsActive = x.IsActive,
                MenusNumber = x.Menus.Count,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                PostalCode = x.PostalCode,
                TodayOrdersNumber = x.Orders.Count,
                WebsiteUrl = x.WebsiteUrl,
                RestaurantImages = x.RestaurantImages
                    .Select(i => i.Url)
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }


    public Restaurant GetRestaurantByIdSync(int restaurantId)
    {
        return _context.Restaurants
            .Where(x => x.Id == restaurantId)
            .FirstOrDefault();
    }

    public async Task<ICollection<RestaurantCardDto>> GetCustomerRestaurants(CustomerQueryParams.RestaurantsQueryParams restaurantsQueryParams)
    {
        var query = _context.Restaurants
            .Where(x => x.IsDeleted == false);

        query = query
            .Skip(restaurantsQueryParams.PageSize * restaurantsQueryParams.PageIndex)
            .Take(restaurantsQueryParams.PageSize);

        return await query
            .Select(x => new RestaurantCardDto
            {
                Address = x.Address,
                City = x.City,
                Country = x.Country.Name,
                Id = x.Id,
                IsOpen = x.IsOpen,
                Name = x.Name,
                ProfileImage = x.RestaurantImages
                    .Where(ri => ri.IsDeleted == false)
                    .Select(ri => ri.Url)
                    .FirstOrDefault()
            })
            .ToListAsync();
    }

    public async Task<CustomerDtos.RestaurantDto> GetCustomerRestaurant(int restaurantId)
    {
        return await _context.Restaurants
            .Where(x => x.IsDeleted == false)
            .Select(x => new CustomerDtos.RestaurantDto
            {
                Address = x.Address,
                City = x.City,
                Country = x.Country.Name,
                Description = x.Description,
                EmployeesNumber = x.Employees.Count,
                FacebookUrl = x.FacebookUrl,
                Id = x.Id,
                InstagramUrl = x.InstagramUrl,
                IsOpen = x.IsOpen,
                MenusNumber = x.Menus.Count,
                Name = x.Name,
                PhoneNumber = x.PhoneNumber,
                PostalCode = x.PostalCode,
                RestaurantImages = x.RestaurantImages
                    .Where(i => i.IsDeleted == false)
                    .Select(i => i.Url)
                    .ToList(),
                WebsiteUrl = x.WebsiteUrl
            })
            .FirstOrDefaultAsync();
    }
}
