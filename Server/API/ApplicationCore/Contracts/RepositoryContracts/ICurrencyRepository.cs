using ApplicationCore.DTOs;

namespace API;

public interface ICurrencyRepository
{
    Task<Currency> GetCurrencyById(int currencyId);
    Task<ICollection<GetCurrencyDto>> GetAllCurrencies();
}
