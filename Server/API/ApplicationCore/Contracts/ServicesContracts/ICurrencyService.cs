using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public interface ICurrencyService
{
    Task<Currency> GetCurrencyById(int currencyId);
    Task<ICollection<GetCurrencyDto>> GetAllCurrencies();
}
