using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public interface ICountryRepository
{
    Task<Country> GetCountryById(int id);
    Task<ICollection<GetCountryDto>> GetAllCountries();
    Task<bool> SaveAllAsync();
}
