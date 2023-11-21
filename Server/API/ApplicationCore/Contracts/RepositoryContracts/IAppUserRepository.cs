namespace API;

public interface IAppUserRepository
{
    Task<ICollection<AppUser>> GetAllUsers();
    Task<bool> SaveAllAsync();
}
