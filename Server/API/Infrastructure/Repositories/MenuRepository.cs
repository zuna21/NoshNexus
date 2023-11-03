

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
                MenuItems = new List<MenuItemCardDto>(),
                RestaurantImage = ""
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
            .Where(x => x.Restaurant.OwnerId == ownerId)
            .Select(m => new MenuCardDto
            {
                Name = m.Name,
                Description = m.Description,
                Id = m.Id,
                IsActive = m.IsActive,
                MenuItemNumber = 0,             // Kad bude menu itemsa
                RestaurantName = m.Restaurant.Name
            })
            .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
