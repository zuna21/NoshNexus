namespace API;

public interface IMenuItemRepository
{
    void AddMenuItem(MenuItem menuItem);
    Task<MenuItemDetailsDto> GetMenuItem(int menuItemId, int ownerId);
    Task<GetMenuItemEditDto> GetMenuItemEdit(int menuItemId, int ownerId);
    Task<bool> SaveAllAsync();
}
