using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface ICustomerRepository
{
    void Create(Customer customer);
    Task<Customer> GetCustomerByUsername(string username);
    Task<bool> SaveAllAsync();


}

