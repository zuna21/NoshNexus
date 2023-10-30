
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

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
