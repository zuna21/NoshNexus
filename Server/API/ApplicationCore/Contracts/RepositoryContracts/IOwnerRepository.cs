using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public interface IOwnerRepository
{
    void Create(Owner owner);
    Task<bool> DoesOwnerExists(string username);
    Task<Owner> GetOwnerByUsername(string username);
    Task<GetOwnerEditDto> GetOwnerEdit(string username);
    Task<GetOwnerDto> GetOwnerDetails(string username);
    Task<bool> SaveAllAsync();
}
