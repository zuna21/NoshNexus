namespace API;

public interface IMenuService
{
    Task<Response<string>> Create(CreateMenuDto createMenuDto);
    Task<Response<ICollection<MenuCardDto>>> GetMenus();
    Task<Response<MenuDetailsDto>> GetMenu(int menuId);
    Task<Response<GetMenuEditDto>> GetMenuEdit(int menuId);



    // globalne funkcije
    Task<Menu> GetMenuById(int menuId);
}
