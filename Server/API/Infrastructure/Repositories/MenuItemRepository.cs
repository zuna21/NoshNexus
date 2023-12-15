
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly DataContext _context;
    public MenuItemRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void AddMenuItem(MenuItem menuItem)
    {
        _context.Add(menuItem);
    }

    public async Task<GetMenuItemEditDto> GetMenuItemEdit(int menuItemId, int ownerId)
    {
        return await _context.MenuItems
            .Where(x => x.Id == menuItemId && x.Menu.Restaurant.OwnerId == ownerId)
            .Select(m => new GetMenuItemEditDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                HasSpecialOffer = m.HasSpecialOffer,
                IsActive = m.IsActive,
                Price = m.Price,
                SpecialOfferPrice = m.SpecialOfferPrice,
                ProfileImage = m.MenuItemImages
                    .Where(x => x.IsDeleted == false && x.Type == MenuItemImageType.Profile)
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

    public async Task<MenuItemDetailsDto> GetMenuItem(int menuItemId, int ownerId)
    {
        return await _context.MenuItems
            .Where(x => x.Id == menuItemId && x.Menu.Restaurant.OwnerId == ownerId)
            .Select(m => new MenuItemDetailsDto
            {
                Id = m.Id,
                Name = m.Name,
                Description = m.Description,
                HasSpecialOffer = m.HasSpecialOffer,
                Price = m.Price,
                SpecialOfferPrice = m.SpecialOfferPrice,
                IsActive = m.IsActive,
                Image = m.MenuItemImages
                    .Where(x => x.IsDeleted == false && x.Type == MenuItemImageType.Profile)
                    .Select(x => x.Url)
                    .FirstOrDefault(),
                TodayOrders = 0
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<MenuItem> GetOwnerMenuItem(int menuItemId, int ownerId)
    {
        return await _context.MenuItems
            .FirstOrDefaultAsync(x => x.Id == menuItemId && x.Menu.Restaurant.OwnerId == ownerId);
    }

    public async Task<ICollection<MenuItem>> GetRestaurantMenuItems(ICollection<int> menuItemIds, int restaurantId)
    {
        return await _context.MenuItems
            .Where(x => menuItemIds.Contains(x.Id) && x.Menu.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public async Task<MenuItemDetailsDto> GetEmployeeMenuItem(int menuItemId, int restaurantId)
    {
        return await _context.MenuItems
            .Where(x => 
                x.IsDeleted == false && 
                x.Id == menuItemId && 
                x.Menu.RestaurantId == restaurantId
            )
            .Select(x => new MenuItemDetailsDto
            {
                Description = x.Description,
                HasSpecialOffer = x.HasSpecialOffer,
                Id = x.Id,
                Image = x.MenuItemImages
                    .Where(m => m.IsDeleted == false && m.Type == MenuItemImageType.Profile)
                    .Select(m => m.Url)
                    .FirstOrDefault(),
                IsActive = x.IsActive,
                Name = x.Name,
                Price = x.Price,
                SpecialOfferPrice =x.SpecialOfferPrice,
                TodayOrders = 1000 // zamijeniti sa pravom vrijednoscu
            })
            .FirstOrDefaultAsync();
    }

    public async Task<GetMenuItemEditDto> GetEmployeeMenuItemEdit(int menuItemId, int restaurantId)
    {
        return await _context.MenuItems
            .Where(x => 
                x.IsDeleted == false &&
                x.Id == menuItemId &&
                x.Menu.RestaurantId == restaurantId
            )
            .Select(x => new GetMenuItemEditDto
            {
                Id = x.Id,
                Description = x.Description,
                HasSpecialOffer = x.HasSpecialOffer,
                IsActive = x.IsActive,
                Name = x.Name,
                Price = x.Price,
                ProfileImage = x.MenuItemImages
                    .Where(i => i.IsDeleted == false && i.Type == MenuItemImageType.Profile)
                    .Select(i => new ImageDto
                    {
                        Id = i.Id,
                        Size = i.Size,
                        Url = i.Url
                    })
                    .FirstOrDefault(),
                SpecialOfferPrice = x.SpecialOfferPrice
            })
            .FirstOrDefaultAsync();
    }

    public async Task<MenuItem> GetEmployeeMenuItemEntity(int menuItemId, int restaurantId)
    {
        return await _context.MenuItems
            .Where(x => 
                x.Id == menuItemId &&
                x.IsDeleted == false &&
                x.Menu.RestaurantId == restaurantId
            )
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<MenuItemRowDto>> GetCustomerRestaurantMenuItems(int restaurantId, string sq)
    {
        return await _context.MenuItems
            .Where(x => x.Menu.RestaurantId == restaurantId && x.IsDeleted == false && x.IsActive == true)
            .Where(x => x.Name.ToLower().Contains(sq.ToLower()))
            .Select(x => new MenuItemRowDto
            {
                Description = x.Description,
                HasSpecialOffer = x.HasSpecialOffer,
                Id = x.Id, 
                Images = x.MenuItemImages
                    .Where(i => i.IsDeleted == false)
                    .Select(i => i.Url)
                    .ToList(),
                Menu = new MenuMenuItemRowDto
                {
                    Id = x.MenuId,
                    Name = x.Menu.Name
                },
                Name = x.Name,
                Price = x.Price,
                ProfileImage = x.MenuItemImages
                    .Where(i => i.IsDeleted == false && i.Type == MenuItemImageType.Profile)
                    .Select(i => i.Url)
                    .FirstOrDefault(),
                RestaurantId = x.Menu.RestaurantId,
                SpecialOfferPrice = x.SpecialOfferPrice
            })
            .ToListAsync();
    }

}
