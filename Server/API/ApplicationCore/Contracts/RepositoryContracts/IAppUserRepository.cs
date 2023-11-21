namespace API;

public interface IAppUserRepository
{
    Task<ICollection<AppUser>> GetAllUsers();
    Task<AppUser> GetUserByUsername(string username);
    Task<bool> SaveAllAsync();
}
