namespace API;

public interface ICustomerRepository
{
    void Create(Customer customer);
    Task<bool> SaveAllAsync();
}
