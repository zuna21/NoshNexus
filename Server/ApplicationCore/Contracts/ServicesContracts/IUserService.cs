using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IUserService
{
    // globalna funkcija
    Task<AppUser> GetUser();
}
