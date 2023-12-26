using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IAppUserRepository
{
    Task<ICollection<AppUser>> GetAllUsers();
    Task<AppUser> GetUserByUsername(string username);
    Task<AppUser> GetUserById(int userId);
    Task<AppUser> GetAppUserByCustomerId(int customerId);
    Task<bool> SaveAllAsync();

    // Sync function for hubs
    AppUser GetUserByUsernameSync(string username);
}
