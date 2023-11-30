using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IMenuItemImageRepository
{
    void AddImage(MenuItemImage image);
    Task<MenuItemImage> GetProfileImage(int menuItemId);
    Task<bool> SaveAllAsync();

    Task<MenuItemImage> GetOwnerMenuItemImage(int menuItemImageId, int ownerId);
}