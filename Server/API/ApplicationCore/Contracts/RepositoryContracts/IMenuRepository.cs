namespace API;

public interface IMenuRepository
{
    void AddMenu(Menu menu);
    Task<ICollection<MenuCardDto>> GetMenus(int ownerId);
    Task<MenuDetailsDto> GetMenu(int menuId, int ownerId);
    Task<GetMenuEditDto> GetMenuEdit(int menuId, int ownerId);
    Task<bool> SaveAllAsync();


    // For global
    Task<Menu> GetOwnerMenu(int menuId, int ownerId);
}
