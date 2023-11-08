namespace API;

public interface ICurrencyService
{
    Task<Currency> GetCurrencyById(int currencyId);
}
