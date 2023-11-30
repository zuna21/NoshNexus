using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IOwnerImageRepository
{
    void AddImage(OwnerImage image);
    Task<OwnerImage> GetProfileImage(int ownerId);
    Task<bool> SaveAllAsync();
}

