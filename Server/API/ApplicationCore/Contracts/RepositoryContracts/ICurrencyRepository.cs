namespace API;

public interface ICurrencyRepository
{
    Task<Currency> GetCurrencyById(int currencyId);
}
