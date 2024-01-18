using ApplicationCore.Entities;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface ICountryRepository
{
    Task<Country> GetCountryById(int id);
    Task<ICollection<OwnerDtos.GetCountryDto>> GetAllCountries();
    Task<bool> SaveAllAsync();
}
