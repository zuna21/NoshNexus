
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class MenuItemImageRepository : IMenuItemImageRepository
{
    private readonly DataContext _context;
    public MenuItemImageRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void AddImage(MenuItemImage image)
    {
        _context.MenuItemImages.Add(image);
    }

    public async Task<MenuItemImage> GetOwnerMenuItemImage(int menuItemImageId, int ownerId)
    {
        return await _context.MenuItemImages
            .FirstOrDefaultAsync(
                x => 
                x.Id == menuItemImageId &&
                x.IsDeleted == false &&
                x.MenuItem.Menu.Restaurant.OwnerId == ownerId
            );
    }

    public async Task<MenuItemImage> GetProfileImage(int menuItemId)
    {
        return await _context.MenuItemImages
            .FirstOrDefaultAsync(
                x => 
                x.IsDeleted == false && 
                x.MenuItemId == menuItemId && 
                x.Type == MenuItemImageType.Profile
            );
    }

    public async Task<MenuItemImage> GetRestaurantMenuItemImage(int menuItemImageId, int restaurantId)
    {
        return await _context.MenuItemImages
            .Where(x => x.Id == menuItemImageId && x.MenuItem.Menu.RestaurantId == restaurantId)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
