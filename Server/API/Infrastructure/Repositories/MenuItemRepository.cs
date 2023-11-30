
using ApplicationCore.DTOs;
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
}
