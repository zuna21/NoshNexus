using ApplicationCore.Entities;

namespace ApplicationCore;

public interface IAccountService
{
    Task<Owner> GetOwner();
    Task<Employee> GetEmployee();
    Task<Customer> GetCustomer();
}
