namespace API;

public interface IMenuRepository
{
    void AddMenu(Menu menu);
    Task<ICollection<MenuCardDto>> GetOwnerMenus(int ownerId);
    Task<bool> SaveAllAsync();
}
