namespace API;

public interface IMenuItemService
{
    Task<Response<string>> Create(int menuId, CreateMenuItemDto createMenuItemDto); 
}
