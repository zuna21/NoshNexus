namespace API;

public interface IMenuService
{
    Task<Response<string>> Create(CreateMenuDto createMenuDto);
    Task<Response<ICollection<MenuCardDto>>> GetOwnerMenus();
    Task<Response<MenuDetailsDto>> GetMenuDetails(int menuId);
    Task<Response<GetMenuEditDto>> GetMenuEdit(int menuId);
}
