using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface ICurrencyRepository
{
    Task<Currency> GetCurrencyById(int currencyId);
    Task<ICollection<GetCurrencyDto>> GetAllCurrencies();
}

