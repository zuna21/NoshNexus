using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface ITokenService
{
    string CreateToken(AppUser user, string role);
}
