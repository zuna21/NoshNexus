using ApplicationCore.DTOs;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface ICustomerService
{
    Task<Response<CustomerDtos.AccountDto>> Register(CustomerDtos.ActivateAccountDto registerCustomerDto);    
    Task<Response<CustomerDtos.AccountDto>> Login(CustomerDtos.LoginDto loginCustomerDto);
    Task<Response<CustomerDtos.AccountDto>> LoginAsGuest();
    Task<Response<bool>> ActivateAccount(CustomerDtos.ActivateAccountDto activateAccountDto);

}