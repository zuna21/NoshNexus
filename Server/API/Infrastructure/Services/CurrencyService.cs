

using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public class CurrencyService : ICurrencyService
{
    private readonly ICurrencyRepository _currencyRepository;
    public CurrencyService(
        ICurrencyRepository currencyRepository
    )
    {
        _currencyRepository = currencyRepository;
    }

    public async Task<ICollection<GetCurrencyDto>> GetAllCurrencies()
    {
        try
        {
            var currencies = await _currencyRepository.GetAllCurrencies();
            return currencies;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return new List<GetCurrencyDto>();
        }
    }

    public async Task<Currency> GetCurrencyById(int currencyId)
    {
        try
        {
            var currency  = await _currencyRepository.GetCurrencyById(currencyId);
            return currency;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }
    }
}
