using System.Security.Claims;
using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.Entities;

namespace API;

public class AccountService : IAccountService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOwnerRepository _ownerRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ICustomerRepository _customerRepository;
    public AccountService(
        IHttpContextAccessor httpContextAccessor,
        IOwnerRepository ownerRepository,
        IEmployeeRepository employeeRepository,
        ICustomerRepository customerRepository
    )
    {
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
}
