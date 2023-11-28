namespace API;

public interface IAppUserImageRepository
{
    void AddImage(AppUserImage image);
    Task<AppUserImage> GetProfileImage(int userId);
    Task<ImageDto> GetUserProfileImage(int userId);
    Task<bool> SaveAllAsync();
}
