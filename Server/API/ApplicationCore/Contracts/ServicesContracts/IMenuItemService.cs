namespace API;

public interface IMenuItemService
{
    Task<Response<MenuItemCardDto>> Create(int menuId, CreateMenuItemDto createMenuItemDto); 
    Task<Response<MenuItemDetailsDto>> GetMenuItem(int menuItemId);
    Task<Response<GetMenuItemEditDto>> GetMenuItemEdit(int menuItemId);
}
