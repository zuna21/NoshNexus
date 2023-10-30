
namespace API;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;
    public CountryService(
        ICountryRepository countryRepository
    )
    {
        _countryRepository = countryRepository;
    }
    public async Task<Country> GetCountryById(int id)
    {
        Country country = new();
        try
        {
            country = await _countryRepository.GetCountryById(id);
        }
        catch (Exception ex)
        {
            country = null;
            Console.WriteLine(ex.ToString());
        }

        return country;
    }
}
