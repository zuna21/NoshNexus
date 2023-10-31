namespace API;

public interface IMenuItemService
{
    Task<Response<string>> Create(int menuId, CreateMenuItemDto createMenuItemDto); 
    Task<Response<MenuItemDetailsDto>> GetMenuItemDetails(int menuItemId);
    Task<Response<GetMenuItemEditDto>> GetMenuItemEdit(int menuItemId);
}
