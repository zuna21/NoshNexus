namespace API;

public interface IMenuRepository
{
    void AddMenu(Menu menu);
    Task<ICollection<MenuCardDto>> GetOwnerMenus(int ownerId);
    Task<MenuDetailsDto> GetMenuDetails(int menuId, int ownerId);
    Task<GetMenuEditDto> GetMenuEdit(int menuId, int ownerId);
    Task<bool> SaveAllAsync();
}
