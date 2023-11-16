

using Microsoft.EntityFrameworkCore;

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
                RestaurantId = m.RestaurantId
            }).FirstOrDefaultAsync();
    }

    public async Task<Menu> GetOwnerMenu(int menuId, int ownerId)
    {
        return await _context.Menus.FirstOrDefaultAsync(x => x.Id == menuId && x.Restaurant.OwnerId == ownerId);
    }

    public async Task<ICollection<MenuCardDto>> GetMenus(int ownerId)
    {
        return await _context.Menus
            .Where(x => x.Restaurant.OwnerId == ownerId && x.IsDeleted == false)
            .Select(m => new MenuCardDto
            {
                Name = m.Name,
                Description = m.Description,
                Id = m.Id,
                IsActive = m.IsActive,
                MenuItemNumber = m.MenuItems.Count,             // Kad bude menu itemsa
                RestaurantName = m.Restaurant.Name
            })
            .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
