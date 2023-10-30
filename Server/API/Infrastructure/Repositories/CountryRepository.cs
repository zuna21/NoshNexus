
namespace API;

public class CountryRepository : ICountryRepository
{
    private readonly DataContext _context;
    public CountryRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public async Task<Country> GetCountryById(int id)
    {
        return await _context.Countries.FindAsync(id);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
