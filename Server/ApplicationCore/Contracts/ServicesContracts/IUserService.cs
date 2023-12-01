using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IUserService
{
    // globalna funkcija
    Task<AppUser> GetUser();
    Task<Owner> GetOwner();
    Task<Employee> GetEmployee();
    Task<Customer> GetCustomer();
}
