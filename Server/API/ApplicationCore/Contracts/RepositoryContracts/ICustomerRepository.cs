namespace API;

public interface ICustomerRepository
{
    void Create(Customer customer);
    Task<Customer> GetCustomerByUsername(string username);
    Task<bool> SaveAllAsync();
}
