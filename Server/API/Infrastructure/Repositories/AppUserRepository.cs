﻿
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

    public async Task<AppUser> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}