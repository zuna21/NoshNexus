using ApplicationCore.Entities;

namespace API;

public interface IMenuItemImageRepository
{
    void AddImage(MenuItemImage image);
    Task<MenuItemImage> GetProfileImage(int menuItemId);
    Task<bool> SaveAllAsync();

    Task<MenuItemImage> GetOwnerMenuItemImage(int menuItemImageId, int ownerId);
}
