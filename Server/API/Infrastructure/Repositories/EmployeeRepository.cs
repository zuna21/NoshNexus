

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

    public async Task<EmployeeDetailsDto> GetEmployee(int employeeId, int ownerId)
    {
        return await _context.Employees
            .Where(x => x.Id == employeeId && x.Restaurant.OwnerId == ownerId)
            .Select(e => new EmployeeDetailsDto
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Birth = e.Birth,
                CanEditFolders = e.CanEditFolders,
                CanEditMenus = e.CanEditMenus,
                CanViewFolders = e.CanViewFolders,
                Description = e.Description,
                Email = e.AppUser.Email,
                PhoneNumber = e.AppUser.PhoneNumber,
                ProfileImage = e.EmployeeImages
                    .Where(x => x.IsDeleted == false && x.Type == EmployeeImageType.Profile)
                    .Select(x => x.Url)
                    .FirstOrDefault(),
                Username = e.UniqueUsername,
                Restaurant = new EmployeeEditRestaurantDto
                {
                    Id = e.RestaurantId,
                    Name = e.Restaurant.Name,
                    ProfileImage = e.Restaurant.RestaurantImages
                        .Where(x => x.IsDeleted == false && x.Type == RestaurantImageType.Profile)
                        .Select(x => x.Url)
                        .FirstOrDefault()
                }
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Employee> GetOwnerEmployee(int employeeId, int ownerId)
    {
        return await _context.Employees.FirstOrDefaultAsync(x => x.Id == employeeId && x.Restaurant.OwnerId == ownerId);
    }

    public async Task<GetEmployeeEditDto> GetEmployeeEdit(int employeeId, int ownerId)
    {
        return await _context.Employees
            .Where(x => x.Id == employeeId && x.Restaurant.OwnerId == ownerId)
            .Select(e => new GetEmployeeEditDto
            {
                Address = e.Address,
                Birth = e.Birth,
                CanEditFolders = e.CanEditFolders,
                CanEditMenus = e.CanEditMenus,
                CanViewFolders = e.CanViewFolders,
                City = e.City,
                Description = e.Description,
                Email = e.AppUser.Email,
                FirstName = e.FirstName,
                Id = e.Id,
                LastName = e.LastName,
                PhoneNumber = e.AppUser.PhoneNumber,
                Username = e.UniqueUsername,
                RestaurantId = e.RestaurantId,
                ProfileImage = e.EmployeeImages
                    .Where(x => x.Type == EmployeeImageType.Profile && x.IsDeleted == false)
                    .Select(x => new ImageDto
                    {
                        Id = x.Id,
                        Size = x.Size,
                        Url = x.Url
                    })
                    .FirstOrDefault(),
                OwnerRestaurants = new List<RestaurantSelectDto>()
            })
            .FirstOrDefaultAsync();
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
                ProfileImage = e.EmployeeImages
                    .Where(x => x.IsDeleted == false && x.Type == EmployeeImageType.Profile)
                    .Select(x => x.Url)
                    .FirstOrDefault(),
                Username = e.UniqueUsername,
                Restaurant = new EmployeeCardRestaurantDto
                {
                    Id = e.RestaurantId,
                    Name = e.Restaurant.Name,
                    ProfileImage = e.Restaurant.RestaurantImages
                        .Where(x => x.IsDeleted == false && x.Type == RestaurantImageType.Profile)
                        .Select(x => x.Url)
                        .FirstOrDefault()
                }
            }).ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
