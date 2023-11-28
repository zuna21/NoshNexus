
namespace API;

public class CustomerRepository : ICustomerRepository
{
    private readonly DataContext _context;
    public CustomerRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void Create(Customer customer)
    {
        _context.Customers.Add(customer);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
