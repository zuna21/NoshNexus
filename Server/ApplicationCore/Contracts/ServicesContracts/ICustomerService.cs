using ApplicationCore.DTOs;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface ICustomerService
{
    Task<Response<CustomerDto>> Register(RegisterCustomerDto registerCustomerDto);    
    Task<Response<CustomerDto>> Login(LoginCustomerDto loginCustomerDto);

    Task<Response<CustomerDto>> LoginAsGuest();

}