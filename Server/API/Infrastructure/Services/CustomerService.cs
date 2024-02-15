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
    IUserService userService,
    IAppUserImageRepository appUserImageRepository,
    ICountryRepository countryRepository
    ) : ICustomerService
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUserService _userService = userService;
    private readonly IAppUserImageRepository _appUserImageRepository = appUserImageRepository;
    private readonly ICountryRepository _countryRepository = countryRepository;

    public async Task<Response<AccountDto>> ActivateAccount(ActivateAccountDto activateAccountDto)
    {
        Response<AccountDto> response = new();
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
            response.Data = new AccountDto
            {
                ProfileImage = await _appUserImageRepository.GetProfileImageUrl(user.Id),
                Token = _tokenService.CreateToken(user, "customer"),
                Username = user.UserName
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

    public async Task<Response<AccountDto>> EditAccount(EditAccountDto editAccountDto)
    {
        Response<AccountDto> response = new();
        try
        {
            var customer = await _userService.GetCustomer();
            var user = await _userService.GetUser();
            if (customer == null || user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            if (
                !string.Equals(editAccountDto.FirstName, customer.FirstName) ||
                !string.Equals(editAccountDto.LastName, customer.LastName) || 
                !string.Equals(editAccountDto.Description, customer.Description) || 
                !string.Equals(editAccountDto.City, customer.City)
            )
            {
                customer.FirstName = editAccountDto.FirstName;
                customer.LastName = editAccountDto.LastName;
                customer.Description = editAccountDto.Description;
                customer.City = editAccountDto.City;

                if (!await _customerRepository.SaveAllAsync())
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Failed to update account.";
                    return response;
                }
            }

            if (customer.CountryId != editAccountDto.CountryId && editAccountDto.CountryId != -1)
            {
                var country = await _countryRepository.GetCountryById(editAccountDto.CountryId);
                if (country == null)
                {
                    response.Status = ResponseStatus.NotFound;
                    return response;
                }
                customer.CountryId = country.Id;
                customer.Country = country;
                if (!await _customerRepository.SaveAllAsync())
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Failed to update country";
                    return response;
                }
            }

            if (!string.Equals(customer.UniqueUsername, editAccountDto.Username.ToLower()))
            {
                var userExists = await _userManager.FindByNameAsync(editAccountDto.Username.ToLower());
                if (userExists != null)
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Username is already taken.";
                    return response;
                }

                user.UserName = editAccountDto.Username.ToLower();
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Failed to update username.";
                    return response;
                }
                customer.UniqueUsername = user.UserName;
                if (!await _customerRepository.SaveAllAsync())
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Failed to update username.";
                    return response;
                }
            }

            response.Status = ResponseStatus.Success;
            response.Data = new AccountDto
            {
                ProfileImage = await _appUserImageRepository.GetProfileImageUrl(user.Id),
                Token = _tokenService.CreateToken(user, "customer"),
                Username = user.UserName
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

    public async Task<Response<GetAccountEditDto>> GetAccountEdit()
    {
        Response<GetAccountEditDto> response = new();
        try
        {
            var customer = await _userService.GetCustomer();
            if (customer == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            if (!customer.IsActivated) 
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "You need to activated your account first.";
                return response;
            }

            var countries = await _countryRepository.GetAllCountries();
            var result = await _customerRepository.GetAccountEdit(customer.Id);
            if (result == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            result.Countries = countries;
            response.Status = ResponseStatus.Success;
            response.Data = result;

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
                Token = _tokenService.CreateToken(user, "customer"),
                ProfileImage = await _appUserImageRepository.GetProfileImageUrl(user.Id)
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
                Token = _tokenService.CreateToken(user, "customer"),
                ProfileImage = await _appUserImageRepository.GetProfileImageUrl(user.Id)
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

    public async Task<Response<AccountDto>> RefreshCustomer()
    {
        Response<AccountDto> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.Success;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = new AccountDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user, "customer"),
                ProfileImage = await _appUserImageRepository.GetProfileImageUrl(user.Id)
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
