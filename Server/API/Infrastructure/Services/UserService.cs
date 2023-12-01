
using System.Security.Claims;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.Entities;

namespace API;

public class UserService : IUserService
{
    private readonly IAppUserRepository _appUserRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICustomerRepository _customerRepository;

    public UserService(
        IAppUserRepository appUserRepository,
        IHttpContextAccessor httpContextAccessor,
        IOwnerRepository ownerRepository,
        IEmployeeRepository employeeRepository,
        ICustomerRepository customerRepository
    )
    {
        _appUserRepository = appUserRepository;
        _httpContextAccessor = httpContextAccessor;
        _ownerRepository = ownerRepository;
        _employeeRepository = employeeRepository;
        _customerRepository = customerRepository;
    }

    public async Task<Customer> GetCustomer()
    {
        try
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _customerRepository.GetCustomerByUsername(username);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return null;
    }

    public async Task<Employee> GetEmployee()
    {
        try
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _employeeRepository.GetEmployeeByUsername(username);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return null;
    }

    public async Task<Owner> GetOwner()
    {
        try
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _ownerRepository.GetOwnerByUsername(username);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return null;
    }

    public async Task<AppUser> GetUser()
    {
        try
        {
            var username =_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return await _appUserRepository.GetUserByUsername(username);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return null;
    }
}
