using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public interface ICountryService
{
    Task<Country> GetCountryById(int id);
    Task<ICollection<GetCountryDto>> GetAllCountries();
}
