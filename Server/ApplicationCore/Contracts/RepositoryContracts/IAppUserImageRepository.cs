using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IAppUserImageRepository
{
    void AddImage(AppUserImage image);
    Task<AppUserImage> GetProfileImage(int userId);
    Task<ImageDto> GetUserProfileImage(int userId);
    Task<bool> SaveAllAsync();

    Task<AppUserImage> GetUserImage(int imageId, int userId);
}
