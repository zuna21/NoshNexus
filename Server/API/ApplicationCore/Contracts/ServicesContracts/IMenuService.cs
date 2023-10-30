namespace API;

public interface IMenuService
{
    Task<Response<string>> Create(CreateMenuDto createMenuDto);
    Task<Response<ICollection<MenuCardDto>>> GetOwnerMenus();
}
