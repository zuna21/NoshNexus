using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.DTOs.CustomerDtos;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

namespace API;

public class CustomerService(
    UserManager<AppUser> userManager,
    ICustomerRepository customerRepository,
    ITokenService tokenService,
    IUserService userService
    ) : ICustomerService
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUserService _userService = userService;

    public async Task<Response<bool>> ActivateAccount(ActivateAccountDto activateAccountDto)
    {
        Response<bool> response = new();
        try
        {
            var customer = await _userService.GetCustomer();
            if (customer == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var user = await _userManager.FindByNameAsync(customer.UniqueUsername);
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var userExists = await _userManager.FindByNameAsync(activateAccountDto.Username.ToLower());
            if (userExists != null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Username is taken.";
                return response;
            }

            if (!string.Equals(activateAccountDto.Password.ToLower(), activateAccountDto.RepeatPassword.ToLower()))
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Password and repeat password are not equal.";
                return response;
            }

            user.UserName = activateAccountDto.Username.ToLower();
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to update username.";
                return response;
            }

            var passwordResult = await _userManager.ChangePasswordAsync(user, "NoshNexus21?", activateAccountDto.Password);
            if (!passwordResult.Succeeded)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to update password.";
                return response;
            }

            customer.UniqueUsername = user.UserName;
            customer.IsActivated = true;
            
            if (!await _customerRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to update user.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = true;

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<GetAccountDetailsDto>> GetAccountDetails()
    {
        Response<GetAccountDetailsDto> response = new();
        try
        {
            var customer = await _userService.GetCustomer();
            if (customer == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var customerDetails = await _customerRepository.GetAccountDetails(customer.Id);
            if (customerDetails == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            } 

            response.Status = ResponseStatus.Success;
            response.Data = customerDetails;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<CustomerDtos.AccountDto>> Login(CustomerDtos.LoginDto loginCustomerDto)
    {
        Response<CustomerDtos.AccountDto> response = new();
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
            response.Data = new CustomerDtos.AccountDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user, "customer")
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

    public async Task<Response<CustomerDtos.AccountDto>> LoginAsGuest()
    {
        Response<CustomerDtos.AccountDto> response = new();
        try
        {
            char[] chars = ['q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p', 'a', 's', 'd', 'f', 'g', 'h', 'j', 'k',
            'l', 'z', 'c', 'v', 'b', 'n', 'm'];
            int length = 5;
            Random random = new();
            var username = $"user-{new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray())}";
            bool doesUserExists = true;

            do
            {
                var userExists = await _userManager.FindByNameAsync(username.ToLower());
                if (userExists == null) doesUserExists = false;
            } while (doesUserExists);

            AppUser user = new()
            {
                UserName = username.ToLower()
            };
            var result = await _userManager.CreateAsync(user, "NoshNexus21?");
            if (!result.Succeeded)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create user.";
                return response;
            }

            Customer customer = new()
            {
                AppUserId = user.Id,
                AppUser = user,
                UniqueUsername = user.UserName
            };

            _customerRepository.Create(customer);
            if (!await _customerRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create account.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = new CustomerDtos.AccountDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user, "customer")
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
