namespace API;

public interface IAppUserImageRepository
{
    void AddImage(AppUserImage image);
    Task<AppUserImage> GetProfileImage(int userId);
    Task<bool> SaveAllAsync();
}
