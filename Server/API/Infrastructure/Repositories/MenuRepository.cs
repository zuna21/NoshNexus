using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using EmployeeDtos = ApplicationCore.DTOs.EmployeeDtos;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;
using EmployeeQueryParams = ApplicationCore.QueryParams.EmployeeQueryParams;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;
using ApplicationCore.DTOs.OwnerDtos;

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

    public async Task<OwnerDtos.GetMenuDetailsDto> GetMenu(int menuId, int ownerId, OwnerQueryParams.MenuItemsQueryParams menuItemsQueryParams)
    {
        var menu = await _context.Menus
            .Where(x => x.Id == menuId && x.Restaurant.OwnerId == ownerId)
            .Select(m => new OwnerDtos.GetMenuDetailsDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                RestaurantImage = m.Restaurant.RestaurantImages
                    .Where(x => x.IsDeleted == false && x.Type == RestaurantImageType.Profile)
                    .Select(x => x.Url)
                    .FirstOrDefault() ?? "https://noshnexus.com/images/default/default.png"
            })
            .FirstOrDefaultAsync();

        var query = _context.MenuItems
            .Where(x => x.MenuId == menuId && x.IsDeleted == false);

        if (!string.IsNullOrEmpty(menuItemsQueryParams.Search))
            query = query.Where(x => x.Name.ToLower().Contains(menuItemsQueryParams.Search.ToLower()));

        if (string.Equals(menuItemsQueryParams.Offer, "noSpecialOffer"))
            query = query.Where(x => x.HasSpecialOffer == false);
        if (string.Equals(menuItemsQueryParams.Offer, "specialOffer"))
            query = query.Where(x => x.HasSpecialOffer == true);


        var totalItems = await query.CountAsync();
        var menuItems = query
            .Skip(menuItemsQueryParams.PageSize * menuItemsQueryParams.PageIndex)
            .Take(menuItemsQueryParams.PageSize)
            .Select(x => new OwnerDtos.MenuItemCardDto
            {
                Id = x.Id,
                Description = x.Description,
                HasSpecialOffer = x.HasSpecialOffer,
                Image = x.MenuItemImages
                    .Where(i => i.IsDeleted == false && i.Type == MenuItemImageType.Profile)
                    .Select(i => i.Url)
                    .FirstOrDefault() ?? "https://noshnexus.com/images/default/default.png",
                IsActive = x.IsActive,
                Name = x.Name,
                Price = x.Price,
                SpecialOfferPrice = x.SpecialOfferPrice,
                Currency = x.Menu.Restaurant.Currency.Code
            })
            .ToList();

        var pagedList = new PagedList<OwnerDtos.MenuItemCardDto>
        {
            Result = menuItems,
            TotalItems = totalItems
        };

        menu.MenuItems = pagedList;
        return menu;
    }

    public async Task<OwnerDtos.GetMenuEditDto> GetMenuEdit(int menuId, int ownerId)
    {
        return await _context.Menus
            .Where(x => x.Id == menuId && x.Restaurant.OwnerId == ownerId)
            .Select(m => new OwnerDtos.GetMenuEditDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                IsActive = m.IsActive,
                RestaurantId = m.RestaurantId,
                OwnerRestaurants = m.Restaurant.Owner.Restaurants
                    .Select(or => new OwnerDtos.GetRestaurantForSelectDto
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

    public async Task<PagedList<OwnerDtos.MenuCardDto>> GetMenus(int ownerId, OwnerQueryParams.MenusQueryParams menusQueryParams)
    {
        var query = _context.Menus
            .Where(x => x.Restaurant.OwnerId == ownerId && x.IsDeleted == false);

        if (!menusQueryParams.Search.IsNullOrEmpty())
            query = query.Where(x => x.Name.ToLower().Contains(menusQueryParams.Search.ToLower()));

        if (string.Equals(menusQueryParams.Activity.ToLower(), "active"))
            query = query.Where(x => x.IsActive == true);

        if (string.Equals(menusQueryParams.Activity.ToLower(), "inactive"))
            query = query.Where(x => x.IsActive == false);

        if (menusQueryParams.Restaurant != -1)
            query = query.Where(x => x.RestaurantId == menusQueryParams.Restaurant);

        var totalItems = await query.CountAsync();

        var result = await query
            .Skip(menusQueryParams.PageSize * menusQueryParams.PageIndex)
            .Take(menusQueryParams.PageSize)
            .Select(m => new OwnerDtos.MenuCardDto
            {
                Name = m.Name,
                Description = m.Description,
                Id = m.Id,
                IsActive = m.IsActive,
                MenuItemNumber = m.MenuItems
                    .Where(x => x.IsDeleted == false)
                    .Count(),
                RestaurantName = m.Restaurant.Name
            })
            .ToListAsync();

        return new PagedList<OwnerDtos.MenuCardDto>
        {
            TotalItems = totalItems,
            Result = result
        };
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<PagedList<OwnerDtos.MenuCardDto>> GetEmployeeMenuCardDtos(int restaurantId, EmployeeQueryParams.MenusQueryParams menusQueryParams)
    {
        var query = _context.Menus
            .Where(x => x.RestaurantId == restaurantId && x.IsDeleted == false);

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
            .Select(x => new OwnerDtos.MenuCardDto
            {
                Id = x.Id,
                Description = x.Description,
                IsActive = x.IsActive,
                MenuItemNumber = x.MenuItems.Count,
                Name = x.Name,
                RestaurantName = x.Restaurant.Name
            }).ToListAsync();

        return new PagedList<OwnerDtos.MenuCardDto>()
        {
            Result = result,
            TotalItems = totalItems
        };


    }

    public async Task<OwnerDtos.GetMenuDetailsDto> GetEmployeeMenu(int menuId, int restaurantId)
    {
        return await _context.Menus
            .Where(x => x.Id == menuId && x.RestaurantId == restaurantId && x.IsDeleted == false)
            .Select(x => new OwnerDtos.GetMenuDetailsDto
            {
                Id = x.Id,
                Description = x.Description,

                // pogledati funkciju getMenu(...)

                /* MenuItems = x.MenuItems
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
                    .ToList(), */
                Name = x.Name,
                RestaurantImage = x.Restaurant.RestaurantImages
                    .Where(i => i.IsDeleted == false && i.Type == RestaurantImageType.Profile)
                    .Select(i => i.Url)
                    .FirstOrDefault() ?? "https://noshnexus.com/images/default/default.png"
            })
            .FirstOrDefaultAsync();
    }

    public async Task<EmployeeDtos.GetMenuEditDto> GetEmployeeMenuEdit(int menuId, int restaurantId)
    {
        return await _context.Menus
            .Where(x =>
                x.IsDeleted == false &&
                x.Id == menuId &&
                x.RestaurantId == restaurantId
            )
            .Select(x => new EmployeeDtos.GetMenuEditDto
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


    public async Task<ICollection<OwnerDtos.GetRestaurantMenusForSelectDto>> GetRestaurantMenusForSelect(int restaurantId, int ownerId)
    {
        return await _context.Menus
            .Where(x => x.IsDeleted == false && x.RestaurantId == restaurantId && x.Restaurant.OwnerId == ownerId)
            .Select(x => new OwnerDtos.GetRestaurantMenusForSelectDto
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }

    public async Task<ICollection<CustomerDtos.MenuCardDto>> GetCustomerRestaurantMenus(int restaurantId, CustomerQueryParams.MenusQueryParams menusQueryParams)
    {
        var query = _context.Menus
            .Where(x => x.IsDeleted == false && x.RestaurantId == restaurantId);

        if (!string.IsNullOrEmpty(menusQueryParams.Search))
        {
            query = query
                .Where(x => x.Name.ToLower().Contains(menusQueryParams.Search.ToLower()));
        }

        query = query.OrderBy(x => x.Id);

        query = query
            .Skip(menusQueryParams.PageSize * menusQueryParams.PageIndex)
            .Take(menusQueryParams.PageSize);

        return await query
            .Select(x => new CustomerDtos.MenuCardDto
            {
                Description = x.Description,
                Id = x.Id,
                MenuItemNumber = x.MenuItems.Count,
                Name = x.Name,
                RestaurantName = x.Restaurant.Name
            })
            .ToListAsync();
    }

    public async Task<CustomerDtos.MenuDto> GetCustomerMenu(int menuId)
    {
        return await _context.Menus
            .Where(x => x.IsDeleted == false && x.IsActive == true && x.Id == menuId)
            .Select(x => new CustomerDtos.MenuDto
            {
                Description = x.Description,
                Id = x.Id,
                Restaurant = new CustomerDtos.MenuRestaurant
                {
                    Id = x.Id,
                    Name = x.Name
                },
                TotalMenuItems = x.MenuItems
                    .Where(mi => mi.IsActive == true && mi.IsDeleted == false)
                    .Count()
            })
            .FirstOrDefaultAsync();
    }

}
