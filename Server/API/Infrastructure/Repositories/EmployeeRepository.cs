using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
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
            .Where(x => x.Id == employeeId && x.Restaurant.OwnerId == ownerId && x.IsDeleted == false)
            .Select(e => new EmployeeDetailsDto
            {
                Id = e.Id,
                Birth = e.Birth,
                ProfileHeader = new ProfileHeaderDto
                {
                    BackgroundImage = e.Restaurant.RestaurantImages
                        .Where(x => x.IsDeleted == false && x.Type == RestaurantImageType.Profile)
                        .Select(x => x.Url)
                        .FirstOrDefault(),
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    ProfileImage = e.AppUser.AppUserImages
                        .Where(ui => ui.IsDeleted == false && ui.Type == AppUserImageType.Profile)
                        .Select(ui => ui.Url)
                        .FirstOrDefault(),
                    Username = e.UniqueUsername
                },
                CanEditFolders = e.CanEditFolders,
                CanEditMenus = e.CanEditMenus,
                CanViewFolders = e.CanViewFolders,
                Description = e.Description,
                Email = e.AppUser.Email,
                PhoneNumber = e.AppUser.PhoneNumber,
                Restaurant = e.Restaurant.Name
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
                ProfileImage = e.AppUser.AppUserImages
                    .Where(up => up.IsDeleted == false && up.Type == AppUserImageType.Profile)
                    .Select(up => new ImageDto
                    {
                        Id = up.Id,
                        Size = up.Size,
                        Url = up.Url
                    })
                    .FirstOrDefault(),
                OwnerRestaurants = e.Restaurant.Owner.Restaurants
                    .Select(or => new RestaurantSelectDto
                    {
                        Id = or.Id,
                        Name = or.Name
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PagedList<EmployeeCardDto>> GetEmployees(int ownerId, EmployeesQueryParams employeesQueryParams)
    {
        var query = _context.Employees
            .Where(x => x.Restaurant.OwnerId == ownerId && x.IsDeleted == false);
        
        if(!string.IsNullOrEmpty(employeesQueryParams.Search))
            query = query
                .Where(x => 
                    x.FirstName.ToLower().Contains(employeesQueryParams.Search.ToLower()) || 
                    x.LastName.ToLower().Contains(employeesQueryParams.Search.ToLower()) ||
                    x.UniqueUsername.ToLower().Contains(employeesQueryParams.Search.ToLower())
                );

        
        var totalItems = await query.CountAsync();
        var result = await query
            .Skip(employeesQueryParams.PageSize * employeesQueryParams.PageIndex)
            .Take(employeesQueryParams.PageSize)
            .Select(e => new EmployeeCardDto
            {
                Description = e.Description,
                FirstName = e.FirstName,
                Id = e.Id,
                LastName = e.LastName,
                ProfileImage = e.AppUser.AppUserImages
                    .Where(ui => ui.IsDeleted == false && ui.Type == AppUserImageType.Profile)
                    .Select(ui => ui.Url)
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

        return new PagedList<EmployeeCardDto>
        {
            TotalItems = totalItems,
            Result = result
        };
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public async Task<Employee> GetEmployeeByUsername(string username)
    {
        return await _context.Employees
            .Where(x => string.Equals(x.UniqueUsername, username.ToLower()))
            .FirstOrDefaultAsync();
    }

    public Employee GetEmployeeByUsernameSync(string username)
    {
        return _context.Employees
            .Where(x => string.Equals(x.UniqueUsername, username.ToLower()))
            .FirstOrDefault();
    }
}
