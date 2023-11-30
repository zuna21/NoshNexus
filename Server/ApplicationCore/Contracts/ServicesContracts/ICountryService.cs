using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface ICountryService
{
    Task<Country> GetCountryById(int id);
    Task<ICollection<GetCountryDto>> GetAllCountries();
}
