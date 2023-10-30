using Microsoft.AspNetCore.Identity;

namespace API;

public interface ITokenService
{
    string CreateToken(IdentityUser user);
}
