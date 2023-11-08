
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
