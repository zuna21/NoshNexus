namespace API;

public interface IMenuItemRepository
{
    void AddMenuItem(MenuItem menuItem);
    Task<MenuItemDetailsDto> MenuItemDetails(int menuItemId, int ownerId);
    Task<bool> SaveAllAsync();
}
