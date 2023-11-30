

using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ICollection<GetCurrencyDto>> GetAllCurrencies()
    {
        return await _context.Currencies
            .Select(c => new GetCurrencyDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }

    public async Task<Currency> GetCurrencyById(int currencyId)
    {
        return await _context.Currencies.FindAsync(currencyId);
    }
}
