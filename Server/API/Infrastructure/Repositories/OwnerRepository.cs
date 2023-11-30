
using ApplicationCore.DTOs;
using Microsoft.EntityFrameworkCore;

namespace API;

public class OwnerRepository : IOwnerRepository
{
    private readonly DataContext _context;
    public OwnerRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void Create(Owner owner)
    {
        _context.Add(owner);
    }

    public async Task<bool> DoesOwnerExists(string username)
    {
        return await _context.Owners.AnyAsync(x => x.UniqueUsername == username);
    }

    public async Task<Owner> GetOwnerByUsername(string username)
    {
        return await _context.Owners
            .FirstOrDefaultAsync(x => x.UniqueUsername == username);
    }

    public async Task<GetOwnerDto> GetOwnerDetails(string username)
    {
        return await _context.Owners
            .Where(x => x.UniqueUsername == username)
            .Select(o => new GetOwnerDto
            {
                Address = o.Address,
                Birth = o.Birth,
                City = o.City,
                Country = o.Country.Name,
                Description = o.Description,
                Email = o.AppUser.Email,
                FirstName = o.FirstName,
                LastName = o.LastName,
                Id = o.Id,
                Username = o.UniqueUsername,
                PhoneNumber = o.AppUser.PhoneNumber,
                RestaurantsNumber = o.Restaurants.Where(x => x.IsDeleted == false).Count(),
                ProfileHeader = new ProfileHeaderDto
                {
                    BackgroundImage = o.Restaurants
                        .SelectMany(x => x.RestaurantImages)
                        .Where(x => x.IsDeleted == false && x.Type == RestaurantImageType.Profile)
                        .Select(x => x.Url)
                        .FirstOrDefault(),
                    FirstName = o.FirstName,
                    LastName = o.LastName,
                    ProfileImage = o.AppUser.AppUserImages
                        .Where(x => x.IsDeleted == false && x.Type == AppUserImageType.Profile)
                        .Select(x => x.Url)
                        .FirstOrDefault(),
                    Username = o.UniqueUsername
                },
                EmployeesNumber = o.Restaurants
                    .Where(x => x.IsDeleted == false)
                    .SelectMany(e => e.Employees)
                    .Where(e => e.IsDeleted == false)
                    .Count(),
                MenusNumber = o.Restaurants
                    .Where(x => x.IsDeleted == false)
                    .SelectMany(m => m.Menus)
                    .Where(x => x.IsDeleted == false)
                    .Count()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<GetOwnerEditDto> GetOwnerEdit(string username)
    {
        return await _context.Owners
            .Where(x => x.UniqueUsername == username)
            .Select(o => new GetOwnerEditDto
            {
                Id = o.Id,
                Address = o.Address,
                Birth = o.Birth,
                City = o.City,
                Description = o.Description,
                CountryId = o.CountryId,
                Email = o.AppUser.Email,
                FirstName = o.FirstName,
                LastName = o.LastName,
                PhoneNumber = o.AppUser.PhoneNumber,
                Username = o.UniqueUsername,
                ProfileImage = o.AppUser.AppUserImages
                    .Where(x => x.IsDeleted == false && x.Type == AppUserImageType.Profile)
                    .Select(x => new ImageDto
                    {
                        Id = x.Id,
                        Size = x.Size,
                        Url = x.Url
                    })
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
