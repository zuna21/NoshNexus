using ApplicationCore.DTOs;
using ApplicationCore.DTOs.OwnerDtos;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IUserService
{
    // globalna funkcija
    Task<AppUser> GetUser();
    Task<Owner> GetOwner();
    Task<Employee> GetEmployee();
    Task<Customer> GetCustomer();
    Task<Response<AccountDto>> Login(LoginDto loginDto);
    Task<Response<AccountDto>> RefreshUser();
}
