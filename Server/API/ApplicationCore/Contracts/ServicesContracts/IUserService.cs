namespace API;

public interface IUserService
{
    // globalna funkcija
    Task<AppUser> GetUser();
}
