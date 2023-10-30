namespace API;

public interface ICountryRepository
{
    Task<Country> GetCountryById(int id);
    Task<bool> SaveAllAsync();
}
