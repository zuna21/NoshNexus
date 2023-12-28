

using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace API;

public class MenuRepository : IMenuRepository
{
    private readonly DataContext _context;
    public MenuRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void AddMenu(Menu menu)
    {
        _context.Menus.Add(menu);
    }

    public async Task<MenuDetailsDto> GetMenu(int menuId, int ownerId)
    {
        return await _context.Menus
            .Where(x => x.Id == menuId && x.Restaurant.OwnerId == ownerId)
            .Select(m => new MenuDetailsDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                MenuItems = m.MenuItems
                    .Where(x => x.IsDeleted == false)
                    .Select(x => new MenuItemCardDto
                    {
                        Id = x.Id,
                        Description = x.Description,
                        HasSpecialOffer = x.HasSpecialOffer,
                        Image = x.MenuItemImages
                            .Where(i => i.IsDeleted == false && i.Type == MenuItemImageType.Profile)
                            .Select(i => i.Url)
                            .FirstOrDefault(),
                        IsActive = x.IsActive,
                        Name = x.Name,
                        Price = x.Price,
                        SpecialOfferPrice = x.SpecialOfferPrice
                    })
                    .ToList(),
                RestaurantImage = m.Restaurant.RestaurantImages
                    .Where(x => x.IsDeleted == false && x.Type == RestaurantImageType.Profile)
                    .Select(x => x.Url)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<GetMenuEditDto> GetMenuEdit(int menuId, int ownerId)
    {
        return await _context.Menus
            .Where(x => x.Id == menuId && x.Restaurant.OwnerId == ownerId)
            .Select(m => new GetMenuEditDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                IsActive = m.IsActive,
                RestaurantId = m.RestaurantId,
                OwnerRestaurants = m.Restaurant.Owner.Restaurants
                    .Select(or => new RestaurantSelectDto
                    {
                        Id = or.Id,
                        Name = or.Name
                    })
                    .ToList()
            }).FirstOrDefaultAsync();
    }

    public async Task<Menu> GetOwnerMenu(int menuId, int ownerId)
    {
        return await _context.Menus.FirstOrDefaultAsync(x => x.Id == menuId && x.Restaurant.OwnerId == ownerId);
    }

    public async Task<PagedList<MenuCardDto>> GetMenus(int ownerId, MenusQueryParams menusQueryParams)
    {
        var query = _context.Menus
            .Where(x => x.Restaurant.OwnerId == ownerId && x.IsDeleted == false);

        if (!menusQueryParams.Search.IsNullOrEmpty())
            query = query.Where(x => x.Name.ToLower().Contains(menusQueryParams.Search.ToLower()));
        
        if (string.Equals(menusQueryParams.Activity.ToLower(), "active"))
            query = query.Where(x => x.IsActive == true);

        if (string.Equals(menusQueryParams.Activity.ToLower(), "inactive"))
            query = query.Where(x => x.IsActive == false);
        
        var totalItems = await query.CountAsync();

        var result = await query
            .Skip(menusQueryParams.PageSize * menusQueryParams.PageIndex)
            .Take(menusQueryParams.PageSize)
            .Select(m => new MenuCardDto
            {
                Name = m.Name,
                Description = m.Description,
                Id = m.Id,
                IsActive = m.IsActive,
                MenuItemNumber = m.MenuItems
                    .Where(x => x.IsDeleted == false)
                    .Count(),             // Kad bude menu itemsa
                RestaurantName = m.Restaurant.Name
            })
            .ToListAsync();

        return new PagedList<MenuCardDto>
        {
            TotalItems = totalItems,
            Result = result
        };
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<ICollection<MenuCardDto>> GetEmployeeMenuCardDtos(int restaurantId)
    {
        return await _context.Menus
            .Where(x => x.RestaurantId == restaurantId && x.IsDeleted == false)
            .Select(x => new MenuCardDto
            {
                Id = x.Id,
                Description = x.Description,
                IsActive = x.IsActive,
                MenuItemNumber = x.MenuItems.Count,
                Name = x.Name,
                RestaurantName = x.Restaurant.Name
            })
            .ToListAsync();
    }

    public async Task<MenuDetailsDto> GetEmployeeMenu(int menuId, int restaurantId)
    {
        return await _context.Menus
            .Where(x => x.Id == menuId && x.RestaurantId == restaurantId && x.IsDeleted == false)
            .Select(x => new MenuDetailsDto
            {
                Id = x.Id,
                Description = x.Description,
                MenuItems = x.MenuItems
                    .Where(m => m.IsDeleted == false)
                    .Select(m => new MenuItemCardDto
                    {
                        Description = m.Description,
                        HasSpecialOffer = m.HasSpecialOffer,
                        Id = m.Id,
                        IsActive = m.IsActive,
                        Name = m.Name,
                        Price = m.Price,
                        SpecialOfferPrice = m.SpecialOfferPrice,
                        Image = m.MenuItemImages
                            .Where(i => i.Type == MenuItemImageType.Profile && i.IsDeleted == false)
                            .Select(i => i.Url)
                            .FirstOrDefault()
                    })
                    .ToList(),
                Name = x.Name,
                RestaurantImage = x.Restaurant.RestaurantImages
                    .Where(i => i.IsDeleted == false && i.Type == RestaurantImageType.Profile)
                    .Select(i => i.Url)
                    .FirstOrDefault()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<GetEmployeeMenuEditDto> GetEmployeeMenuEdit(int menuId, int restaurantId)
    {
        return await _context.Menus
            .Where(x => 
                x.IsDeleted == false &&
                x.Id == menuId &&
                x.RestaurantId == restaurantId
            )
            .Select(x => new GetEmployeeMenuEditDto
            {
                Description = x.Description,
                Id = x.Id,
                IsActive = x.IsActive,
                Name = x.Name
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Menu> GetEmployeeMenuEntity(int menuId, int restaurantId)
    {
        return await _context.Menus
            .Where(x => x.Id == menuId && x.RestaurantId == restaurantId && x.IsDeleted == false)
            .FirstOrDefaultAsync();

    }

    public async Task<ICollection<CustomerMenuCardDto>> GetCustomerRestaurantMenus(int restaurantId, string sq)
    {
        return await _context.Menus
            .Where(x => x.IsDeleted == false && x.IsActive == true && x.RestaurantId == restaurantId)
            .Where(x => x.Name.ToLower().Contains(sq.ToLower()))
            .Select(x => new CustomerMenuCardDto
            {
                Description = x.Description,
                Id = x.Id,
                MenuItemNumber = x.MenuItems.Count,
                Name = x.Name,
                RestaurantName = x.Restaurant.Name
            })
            .ToListAsync();
    }

    public async Task<CustomerMenuDetailsDto> GetCustomerMenu(int menuId)
    {
        return await _context.Menus
            .Where(x => x.IsDeleted == false && x.IsActive == true && x.Id == menuId)
            .Select(x => new CustomerMenuDetailsDto
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name,
                RestaurantImage = x.Restaurant.RestaurantImages
                    .Where(ri => ri.IsDeleted == false && ri.Type == RestaurantImageType.Profile)
                    .Select(ri => ri.Url)
                    .FirstOrDefault(),
                MenuItems = x.MenuItems
                    .Where(mi => mi.IsActive == true && mi.IsDeleted == false)
                    .Select(mi => new MenuItemRowDto
                    {
                        Id = mi.Id,
                        Description = mi.Description,
                        HasSpecialOffer = mi.HasSpecialOffer,
                        Price = mi.Price,
                        RestaurantId = mi.Menu.RestaurantId,
                        SpecialOfferPrice = mi.SpecialOfferPrice,
                        Name = mi.Name,
                        ProfileImage = mi.MenuItemImages
                            .Where(mip => mip.IsDeleted == false && mip.Type == MenuItemImageType.Profile)
                            .Select(mip => mip.Url)
                            .FirstOrDefault(),
                        Images = mi.MenuItemImages
                            .Where(mii => mii.IsDeleted == false)
                            .Select(mii => mii.Url)
                            .ToList(),
                        Menu = new MenuMenuItemRowDto
                        {
                            Id = mi.MenuId,
                            Name = mi.Menu.Name
                        }
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }
}
