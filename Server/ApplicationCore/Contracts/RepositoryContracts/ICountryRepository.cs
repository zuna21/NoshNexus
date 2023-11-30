using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface ICountryRepository
{
    Task<Country> GetCountryById(int id);
    Task<ICollection<GetCountryDto>> GetAllCountries();
    Task<bool> SaveAllAsync();
}
