using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

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

    public async Task<OwnerDtos.GetEmployeeDetailsDto> GetEmployee(int employeeId, int ownerId)
    {
        return await _context.Employees
            .Where(x => x.Id == employeeId && x.Restaurant.OwnerId == ownerId && x.IsDeleted == false)
            .Select(e => new OwnerDtos.GetEmployeeDetailsDto
            {
                Id = e.Id,
                Birth = e.Birth,
                ProfileHeader = new OwnerDtos.AccountProfileHeaderDto
                {
                    BackgroundImage = e.Restaurant.RestaurantImages
                        .Where(x => x.IsDeleted == false && x.Type == RestaurantImageType.Profile)
                        .Select(x => x.Url)
                        .FirstOrDefault() ?? "http://localhost:5000/images/default/default.png",
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    ProfileImage = e.AppUser.AppUserImages
                        .Where(ui => ui.IsDeleted == false && ui.Type == AppUserImageType.Profile)
                        .Select(ui => ui.Url)
                        .FirstOrDefault() ?? "http://localhost:5000/images/default/default-profile.png",
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

    public async Task<OwnerDtos.GetEmployeeEditDto> GetEmployeeEdit(int employeeId, int ownerId)
    {
        return await _context.Employees
            .Where(x => x.Id == employeeId && x.Restaurant.OwnerId == ownerId)
            .Select(e => new OwnerDtos.GetEmployeeEditDto
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
                    .Select(or => new OwnerDtos.GetRestaurantForSelectDto
                    {
                        Id = or.Id,
                        Name = or.Name
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PagedList<OwnerDtos.EmployeeCardDto>> GetEmployees(int ownerId, OwnerQueryParams.EmployeesQueryParams employeesQueryParams)
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

        if (employeesQueryParams.Restaurant != -1)
            query = query.Where(x => x.RestaurantId == employeesQueryParams.Restaurant);

        
        var totalItems = await query.CountAsync();
        var result = await query
            .Skip(employeesQueryParams.PageSize * employeesQueryParams.PageIndex)
            .Take(employeesQueryParams.PageSize)
            .Select(e => new OwnerDtos.EmployeeCardDto
            {
                Description = e.Description,
                FirstName = e.FirstName,
                Id = e.Id,
                LastName = e.LastName,
                ProfileImage = e.AppUser.AppUserImages
                    .Where(ui => ui.IsDeleted == false && ui.Type == AppUserImageType.Profile)
                    .Select(ui => ui.Url)
                    .FirstOrDefault() ?? "http://localhost:5000/images/default/default-profile.png",
                Username = e.UniqueUsername,
                Restaurant = new OwnerDtos.EmployeeCardRestaurantDto
                {
                    Id = e.RestaurantId,
                    Name = e.Restaurant.Name,
                    ProfileImage = e.Restaurant.RestaurantImages
                        .Where(x => x.IsDeleted == false && x.Type == RestaurantImageType.Profile)
                        .Select(x => x.Url)
                        .FirstOrDefault() ?? "http://localhost:5000/images/default/default.png"
                }
            }).ToListAsync();

        return new PagedList<OwnerDtos.EmployeeCardDto>
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

    public async Task<ICollection<CustomerDtos.EmployeeCardDto>> GetCustomerEmployees(int restaurantId)
    {
        return await _context.Employees
            .Where(x => x.RestaurantId == restaurantId && x.IsDeleted == false)
            .Select(x => new CustomerDtos.EmployeeCardDto
            {
                Description = x.Description,
                FirstName = x.FirstName,
                Id = x.Id,
                LastName = x.LastName,
                ProfileImage = x.AppUser.AppUserImages
                    .Where(i => i.IsDeleted == false && i.Type == AppUserImageType.Profile)
                    .Select(i => i.Url)
                    .FirstOrDefault() ?? "http://localhost:5000/images/default/default-profile.png",
                Restaurant = new CustomerDtos.EmployeeRestaurantDto
                {
                    Id = x.RestaurantId,
                    Name = x.Restaurant.Name,
                    ProfileImage = x.Restaurant.RestaurantImages
                        .Where(ri => ri.IsDeleted == false && ri.Type == RestaurantImageType.Profile)
                        .Select(ri => ri.Url)
                        .FirstOrDefault() ?? "http://localhost:5000/images/default/default.png"
                },
                Username = x.UniqueUsername
            })
            .ToListAsync();
    }

    public async Task<CustomerDtos.EmployeeDto> GetCustomerEmployee(int employeeId)
    {
        return await _context.Employees
            .Where(x => x.IsDeleted == false && x.Id == employeeId)
            .Select(x => new CustomerDtos.EmployeeDto
            {
                Birth = x.Birth,
                City = x.City,
                Country = x.Country.Name,
                Description = x.Description,
                FirstName = x.FirstName,
                Id = x.Id,
                LastName = x.LastName,
                ProfileImage = x.AppUser.AppUserImages
                    .Where(i => i.IsDeleted == false && i.Type == AppUserImageType.Profile)
                    .Select(i => i.Url)
                    .FirstOrDefault() ?? "http://localhost:5000/images/default/default-profile.png",
                RestaurantImage = x.Restaurant.RestaurantImages
                    .Where(i => i.IsDeleted == false && i.Type == RestaurantImageType.Profile)
                    .Select(i => i.Url)
                    .FirstOrDefault() ?? "http://localhost:5000/images/default/default.png",
                Username = x.UniqueUsername
            })
            .FirstOrDefaultAsync();
    }
}
