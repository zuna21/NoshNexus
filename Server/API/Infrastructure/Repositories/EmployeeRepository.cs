
namespace API;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly DataContext _context;
    public EmployeeRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }
    public void CreateEmployee(Employee employee)
    {
        _context.Employees.Add(employee);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
