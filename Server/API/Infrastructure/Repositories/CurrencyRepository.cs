
namespace API;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly DataContext _context;
    public CurrencyRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public async Task<Currency> GetCurrencyById(int currencyId)
    {
        return await _context.Currencies.FindAsync(currencyId);
    }
}
