namespace API;

public interface ICountryService
{
    Task<Country> GetCountryById(int id);
}
