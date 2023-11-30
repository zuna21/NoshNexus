
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace API;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRestaurantService _restaurantService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IOwnerService _ownerService;
    public EmployeeService(
        IEmployeeRepository employeeRepository,
        IRestaurantService restaurantService,
        UserManager<AppUser> userManager,
        IOwnerService ownerService
    )
    {
        _employeeRepository = employeeRepository;
        _restaurantService = restaurantService;
        _userManager = userManager;
        _ownerService = ownerService;
    }
    public async Task<Response<int>> Create(CreateEmployeeDto createEmployeeDto)
    {
        Response<int> response = new();
        try
        {
            var restaurant = await _restaurantService.GetOwnerRestaurant(createEmployeeDto.RestaurantId);
            if (restaurant == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var isUsernameTaken = await _userManager.FindByNameAsync(createEmployeeDto.Username.ToLower());
            if (isUsernameTaken != null)
            {
                response.Status = ResponseStatus.UsernameTaken;
                response.Message = "Username is taken.";
                return response;
            }

            var user = new AppUser
            {
                UserName = createEmployeeDto.Username.ToLower(),
                Email = createEmployeeDto.Email,
                PhoneNumber = createEmployeeDto.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, createEmployeeDto.Password);
            if (!result.Succeeded)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create an account";
                return response;
            }


            var employee = new Employee
            {
                Address = createEmployeeDto.Address,
                Birth = createEmployeeDto.Birth,
                CanEditFolders = createEmployeeDto.CanEditFolders,
                CanEditMenus = createEmployeeDto.CanEditMenus,
                CanViewFolders = createEmployeeDto.CanViewFolders,
                City = createEmployeeDto.City,
                CountryId = restaurant.CountryId,
                Country = restaurant.Country,
                Description = createEmployeeDto.Description,
                FirstName = createEmployeeDto.FirstName,
                AppUser = user,
                AppUserId = user.Id,
                LastName = createEmployeeDto.LastName,
                Restaurant = restaurant,
                RestaurantId = restaurant.Id,
                UniqueUsername = user.UserName,
            };

            _employeeRepository.CreateEmployee(employee);
            if (!await _employeeRepository.SaveAllAsync())
            {
                await _userManager.DeleteAsync(user);
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create an account.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = employee.Id;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<int>> Delete(int employeeId)
    {
        Response<int> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var employee = await _employeeRepository.GetOwnerEmployee(employeeId, owner.Id);
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            employee.IsDeleted = true;
            if (!await _employeeRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete employee.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = employee.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<EmployeeDetailsDto>> GetEmployee(int id)
    {
        Response<EmployeeDetailsDto> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var employee = await _employeeRepository.GetEmployee(id, owner.Id);
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = employee;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<GetEmployeeEditDto>> GetEmployeeEdit(int id)
    {
        Response<GetEmployeeEditDto> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }


            var employee = await _employeeRepository.GetEmployeeEdit(id, owner.Id);
            if (employee == null) 
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var ownerRestaurants = await _restaurantService.GetRestaurantsForSelect();
            if (ownerRestaurants == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            employee.OwnerRestaurants = ownerRestaurants;
            response.Status = ResponseStatus.Success;
            response.Data = employee;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<ICollection<EmployeeCardDto>>> GetEmployees()
    {
        Response<ICollection<EmployeeCardDto>> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var employees = await _employeeRepository.GetEmployees(owner.Id);
            response.Status = ResponseStatus.Success;
            response.Data = employees;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Employee> GetOwnerEmployee(int employeeId)
    {
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                return null;
            }

            var employee = await _employeeRepository.GetOwnerEmployee(employeeId, owner.Id);
            return employee;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

        return null;
    }

    public async Task<Response<int>> Update(int employeeId, EditEmployeeDto editEmployeeDto)
    {
        Response<int> response = new();
        try
        {
            var owner = await _ownerService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var employee = await _employeeRepository.GetOwnerEmployee(employeeId, owner.Id);
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var user = await _userManager.FindByNameAsync(employee.UniqueUsername);
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            if (employee.RestaurantId != editEmployeeDto.RestaurantId)
            {
                var restaurant = await _restaurantService.GetOwnerRestaurant(editEmployeeDto.RestaurantId);
                if (restaurant == null)
                {
                    response.Status = ResponseStatus.NotFound;
                    return response;
                }

                employee.RestaurantId = restaurant.Id;
                employee.Restaurant = restaurant;
            }

            if (user.UserName != editEmployeeDto.Username.ToLower())
            {
                var userExists = await _userManager.FindByNameAsync(editEmployeeDto.Username.ToLower());
                if (userExists != null)
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Username is taken.";
                    return response;
                }

                user.UserName = editEmployeeDto.Username.ToLower();
            }

            user.Email = editEmployeeDto.Email;
            user.PhoneNumber = editEmployeeDto.PhoneNumber;
            var userUpdated = await _userManager.UpdateAsync(user);
            if (!userUpdated.Succeeded)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to update user.";
                return response;
            }

            // Jos uvijek se password ne moze promijeniti jer mora unijeti i stari password
            // ....

            employee.FirstName = editEmployeeDto.FirstName;
            employee.LastName = editEmployeeDto.LastName;
            employee.Address = editEmployeeDto.Address;
            employee.CanEditMenus = editEmployeeDto.CanEditMenus;
            employee.CanEditFolders = editEmployeeDto.CanEditFolders;
            employee.CanViewFolders = editEmployeeDto.CanViewFolders;
            employee.Birth = editEmployeeDto.Birth;
            employee.Description = editEmployeeDto.Description;
            employee.City = editEmployeeDto.City;
            employee.UniqueUsername = user.UserName;

            if (!await _employeeRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to update employee.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = employee.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }
}
