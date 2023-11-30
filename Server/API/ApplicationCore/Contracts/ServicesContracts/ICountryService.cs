using ApplicationCore.DTOs;

namespace API;

public interface ICountryService
{
    Task<Country> GetCountryById(int id);
    Task<ICollection<GetCountryDto>> GetAllCountries();
}
