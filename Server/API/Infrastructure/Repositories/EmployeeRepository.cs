

using Microsoft.EntityFrameworkCore;

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

    public async Task<ICollection<EmployeeCardDto>> GetEmployees(int ownerId)
    {
        return await _context.Employees
            .Where(x => x.Restaurant.OwnerId == ownerId)
            .Select(e => new EmployeeCardDto
            {
                Description = e.Description,
                FirstName = e.FirstName,
                Id = e.Id,
                LastName = e.LastName,
                ProfileImage = "",
                Username = e.UniqueUsername,
                Restaurant = new EmployeeCardRestaurantDto
                {
                    Id = e.RestaurantId,
                    Name = e.Restaurant.Name,
                    ProfileImage = ""
                }
            }).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
