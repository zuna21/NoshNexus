using ApplicationCore.DTOs;
using ApplicationCore.Entities;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IOwnerRepository
{
    void Create(Owner owner);
    Task<bool> DoesOwnerExists(string username);
    Task<Owner> GetOwnerByUsername(string username);
    Task<OwnerDtos.GetAccountEditDto> GetOwnerEdit(string username);
    Task<OwnerDtos.GetAccountDetailsDto> GetOwnerDetails(string username);
    Task<bool> SaveAllAsync();
}