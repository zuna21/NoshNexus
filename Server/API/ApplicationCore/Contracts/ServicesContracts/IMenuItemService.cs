namespace API;

public interface IMenuItemService
{
    Task<Response<MenuItemCardDto>> Create(int menuId, CreateMenuItemDto createMenuItemDto); 
    Task<Response<int>> Update(int menuItemId, EditMenuItemDto editMenuItemDto);
    Task<Response<MenuItemDetailsDto>> GetMenuItem(int menuItemId);
    Task<Response<GetMenuItemEditDto>> GetMenuItemEdit(int menuItemId);
    Task<Response<int>> Delete(int menuItemId);

    // Globalne funkcije
    Task<MenuItem> GetOwnerMenuItem(int menuItemId);
}
