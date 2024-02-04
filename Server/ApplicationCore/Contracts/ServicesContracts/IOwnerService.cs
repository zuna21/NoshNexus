using ApplicationCore.DTOs;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IOwnerService
{
    Task<Response<OwnerDtos.AccountDto>> Register(OwnerDtos.RegisterDto registerOwnerDto);
    Task<Response<OwnerDtos.AccountDto>> Update(OwnerDtos.EditAccountDto editOwnerDto);
    Task<Response<OwnerDtos.GetAccountDetailsDto>> GetOwnerDetails();
    Task<Response<OwnerDtos.GetAccountEditDto>> GetOwnerEdit();
}