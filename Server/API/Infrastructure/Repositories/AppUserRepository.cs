
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class AppUserRepository : IAppUserRepository
{
    private readonly DataContext _context;
    public AppUserRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public async Task<ICollection<AppUser>> GetAllUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<AppUser> GetAppUserByCustomerId(int customerId)
    {
        return await _context.Users
            .Where(x => x.Customers.Select(c => c.Id).Contains(customerId))
            .FirstOrDefaultAsync();
    }

    public async Task<AppUser> GetUserByUsername(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.UserName == username);
    }

    public AppUser GetUserByUsernameSync(string username)
    {
        return _context.Users
            .Where(x => string.Equals(x.UserName, username.ToLower()))
            .FirstOrDefault();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
