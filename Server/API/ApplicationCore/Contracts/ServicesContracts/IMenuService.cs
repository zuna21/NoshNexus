using ApplicationCore.DTOs;

namespace API;

public interface IMenuService
{
    Task<Response<int>> Create(CreateMenuDto createMenuDto);
    Task<Response<int>> Update(int menuId, EditMenuDto editMenuDto);
    Task<Response<int>> Delete(int menuId);
    Task<Response<ICollection<MenuCardDto>>> GetMenus();
    Task<Response<MenuDetailsDto>> GetMenu(int menuId);
    Task<Response<GetMenuEditDto>> GetMenuEdit(int menuId);



    // globalne funkcije
    Task<Menu> GetOwnerMenu(int menuId);
}
