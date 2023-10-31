namespace API;

public interface IMenuItemRepository
{
    void AddMenuItem(MenuItem menuItem);
    Task<bool> SaveAllAsync();
}
