using ApplicationCore.Entities;

namespace API;

public interface ITokenService
{
    string CreateToken(AppUser user);
}
