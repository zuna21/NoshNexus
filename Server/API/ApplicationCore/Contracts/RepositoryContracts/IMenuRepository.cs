namespace API;

public interface IMenuRepository
{
    void AddMenu(Menu menu);
    Task<ICollection<MenuCardDto>> GetOwnerMenus(Owner owner);
    Task<bool> SaveAllAsync();
}
