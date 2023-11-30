using ApplicationCore.Entities;

namespace API;

public interface IOwnerImageRepository
{
    void AddImage(OwnerImage image);
    Task<OwnerImage> GetProfileImage(int ownerId);
    Task<bool> SaveAllAsync();
}
