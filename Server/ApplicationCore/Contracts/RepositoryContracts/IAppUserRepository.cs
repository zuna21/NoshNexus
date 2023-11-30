using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IAppUserRepository
{
    Task<ICollection<AppUser>> GetAllUsers();
    Task<AppUser> GetUserByUsername(string username);
    Task<bool> SaveAllAsync();
}
