
using Microsoft.AspNetCore.Identity;

namespace API;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IRestaurantService _restaurantService;
    private readonly UserManager<IdentityUser> _userManager;
    public EmployeeService(
        IEmployeeRepository employeeRepository,
        IRestaurantService restaurantService,
        UserManager<IdentityUser> userManager
    )
    {
        _employeeRepository = employeeRepository;
        _restaurantService = restaurantService;
        _userManager = userManager;
    }
    public async Task<Response<string>> Create(CreateEmployeeDto createEmployeeDto)
    {
        Response<string> response = new();
        try
        {
            var restaurant = await _restaurantService.GetOwnerRestaurantById(createEmployeeDto.RestaurantId);
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

            var user = new IdentityUser
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
                IdentityUser = user,
                IdentityUserId = user.Id,
                LastName = createEmployeeDto.LastName,
                Restaurant = restaurant,
                RestaurantId = restaurant.Id,
                UniqueUsername = user.UserName
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
            response.Data = "Successfully created user.";
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }
}
