using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface ICurrencyService
{
    Task<Currency> GetCurrencyById(int currencyId);
    Task<ICollection<GetCurrencyDto>> GetAllCurrencies();
}
