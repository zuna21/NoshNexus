
using System.Security.Claims;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace API;

public class CustomerService : ICustomerService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ICustomerRepository _customerRepository;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    public CustomerService(
        UserManager<AppUser> userManager,
        ICustomerRepository customerRepository,
        ITokenService tokenService,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _userManager = userManager;
        _customerRepository = customerRepository;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
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

    public async Task<Response<CustomerDto>> Login(LoginCustomerDto loginCustomerDto)
    {
        Response<CustomerDto> response = new();
        try
        {
            var user = await _userManager.FindByNameAsync(loginCustomerDto.Username.ToLower());
            if (user == null)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = "Invalid username or password.";
                return response;
            }

            var result = await _userManager.CheckPasswordAsync(user, loginCustomerDto.Password);
            if (!result) 
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = "Invalid username or password.";
                return response;
            }

            var customer = await _customerRepository.GetCustomerByUsername(user.UserName);
            if (customer == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = new CustomerDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<CustomerDto>> Register(RegisterCustomerDto registerCustomerDto)
    {
        Response<CustomerDto> response = new();
        try
        {
            var user = await _userManager.FindByNameAsync(registerCustomerDto.Username.ToLower());
            if (user != null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Username is already taken.";
                return response;
            }

            var appUser = new AppUser
            {
                UserName = registerCustomerDto.Username.ToLower()
            };

            var result = await _userManager.CreateAsync(appUser, registerCustomerDto.Password);
            if (!result.Succeeded)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create account.";
                return response;
            }

            var customer = new Customer
            {
                AppUserId = appUser.Id,
                AppUser = appUser,
                UniqueUsername = appUser.UserName,
            };

            _customerRepository.Create(customer);
            if (!await _customerRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create account.";
                await _userManager.DeleteAsync(user);
                return response;
            }

            var customerDto = new CustomerDto
            {
                Username = appUser.UserName,
                Token = _tokenService.CreateToken(appUser)
            };

            response.Status = ResponseStatus.Success;
            response.Data = new CustomerDto
            {
                Username = appUser.UserName,
                Token = _tokenService.CreateToken(appUser)
            };
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
