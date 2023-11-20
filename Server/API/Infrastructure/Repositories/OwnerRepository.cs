
using Microsoft.EntityFrameworkCore;

namespace API;

public class OwnerRepository : IOwnerRepository
{
    private readonly DataContext _context;
    public OwnerRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void Create(Owner owner)
    {
        _context.Add(owner);
    }

    public async Task<bool> DoesOwnerExists(string username)
    {
        return await _context.Owners.AnyAsync(x => x.UniqueUsername == username);
    }

    public async Task<Owner> GetOwnerByUsername(string username)
    {
        return await _context.Owners
            .FirstOrDefaultAsync(x => x.UniqueUsername == username);
    }

    public async Task<GetOwnerEditDto> GetOwnerEdit(string username)
    {
        return await _context.Owners
            .Where(x => x.UniqueUsername == username)
            .Select(o => new GetOwnerEditDto
            {
                Id = o.Id,
                Address = o.Address,
                Birth = o.Birth,
                City = o.City,
                Description = o.Description,
                CountryId = o.CountryId,
                Email = o.AppUser.Email,
                FirstName = o.FirstName,
                LastName = o.LastName,
                PhoneNumber = o.AppUser.PhoneNumber,
                Username = o.UniqueUsername
            })
            .FirstOrDefaultAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
