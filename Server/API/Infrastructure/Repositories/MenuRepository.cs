

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

    public async Task<ICollection<MenuCardDto>> GetOwnerMenus(int ownerId)
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
