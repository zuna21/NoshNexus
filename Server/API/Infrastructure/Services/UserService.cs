
using System.Security.Claims;
using ApplicationCore.Entities;

namespace API;

public class UserService : IUserService
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(
        IAppUserRepository appUserRepository,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _appUserRepository = appUserRepository;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<AppUser> GetUser()
    {
        try
        {
            var username =_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _appUserRepository.GetUserByUsername(username);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return null;
    }
}
