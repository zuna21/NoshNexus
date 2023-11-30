using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public interface ICurrencyRepository
{
    Task<Currency> GetCurrencyById(int currencyId);
    Task<ICollection<GetCurrencyDto>> GetAllCurrencies();
}
