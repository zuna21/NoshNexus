
using System.Security.Claims;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.DTOs.OwnerDtos;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace API;

public class UserService(
    IAppUserRepository appUserRepository,
    IHttpContextAccessor httpContextAccessor,
    IOwnerRepository ownerRepository,
    IEmployeeRepository employeeRepository,
    ICustomerRepository customerRepository,
    UserManager<AppUser> userManager,
    ITokenService tokenService,
    IAppUserImageRepository appUserImageRepository
    ) : IUserService
{
    private readonly IAppUserRepository _appUserRepository = appUserRepository;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IOwnerRepository _ownerRepository = ownerRepository;
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly ICustomerRepository _customerRepository = customerRepository;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IAppUserImageRepository _appUserImageRepository = appUserImageRepository;

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

    public async Task<Response<AccountDto>> Login(LoginDto loginDto)
    {
        Response<AccountDto> response = new();
        try
        {
            var user = await _userManager.FindByNameAsync(loginDto.Username.ToLower());
            if (user == null)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = Globals.INVALID_USERNAME_OR_PASSWORD;
                return response;
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isPasswordCorrect)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = Globals.INVALID_USERNAME_OR_PASSWORD;
                return response;
            }

            var owner = await _ownerRepository.GetOwnerByUsername(user.UserName);
            var employee = await _employeeRepository.GetEmployeeByUsername(user.UserName);

            if (owner == null && employee == null)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = Globals.INVALID_USERNAME_OR_PASSWORD;
                return response;
            }

            var refreshToken = _tokenService.GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(5);

            await _appUserRepository.SaveAllAsync();

            response.Status = ResponseStatus.Success;
            if (owner != null)
            {
                response.Data = new AccountDto
                {
                    Username = user.UserName,
                    Token = _tokenService.CreateToken(user, "owner"),
                    RefreshToken = user.RefreshToken,
                    ProfileImage = await _appUserImageRepository.GetProfileImageUrl(user.Id)
                };
            }
            if (employee != null)
            {
                response.Data = new AccountDto
                {
                    Username = user.UserName,
                    ProfileImage = await _appUserImageRepository.GetProfileImageUrl(user.Id),
                    RefreshToken = user.RefreshToken,
                    Token = _tokenService.CreateToken(user, "employee")
                };
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<AccountDto>> RefreshToken(RefreshTokenDto refreshTokenDto)
    {
        Response<AccountDto> response = new();
        try
        {
            var owner = await GetOwner();
            var employee = await GetEmployee();
            var user = await GetUser();
            if (owner == null && employee == null)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = Globals.INVALID_USERNAME_OR_PASSWORD;
                return response;
            }

            response.Status = ResponseStatus.Success;
            if (user == null || user.RefreshToken == null)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = Globals.INVALID_USERNAME_OR_PASSWORD;
                return response;
            }

            DateTime currentDate = DateTime.UtcNow;
            if (DateTime.Compare(user.RefreshTokenExpiryTime, currentDate) < 0)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = Globals.EXPIRED_REFRESH_TOKEN;
                return response;
            }

            if (owner != null)
            {
                response.Data = new AccountDto
                {
                    ProfileImage = await _appUserImageRepository.GetProfileImageUrl(user.Id),
                    Username = user.UserName,
                    Token = _tokenService.CreateToken(user, "owner"),
                    RefreshToken = user.RefreshToken
                };
            }
            if (employee != null) 
            {
                response.Data = new AccountDto
                {
                    ProfileImage = await _appUserImageRepository.GetProfileImageUrl(user.Id),
                    Username = user.UserName,
                    RefreshToken = user.RefreshToken,
                    Token = _tokenService.CreateToken(user, "employee")
                };
            }
        }
        catch(Exception ex) 
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }
        return response;
    }

    public async Task<Response<AccountDto>> RefreshUser()
    {
        Response<AccountDto> response = new();
        try
        {
            var user = await GetUser();
            var owner = await GetOwner();
            var employee = await GetEmployee();
            if (owner == null && employee == null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Something went wrong.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            if (owner != null)
            {
                response.Data = new AccountDto
                {
                    ProfileImage = await _appUserImageRepository.GetProfileImageUrl(user.Id),
                    Token = _tokenService.CreateToken(user, "owner"),
                    Username = user.UserName,
                    RefreshToken = user.RefreshToken
                };
            } else {
                response.Data = new AccountDto
                {
                    ProfileImage = await _appUserImageRepository.GetProfileImageUrl(user.Id),
                    Token = _tokenService.CreateToken(user, "employee"),
                    Username = user.UserName,
                    RefreshToken = user.RefreshToken
                };
            }
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

public static class Globals
{
    public const string INVALID_USERNAME_OR_PASSWORD = "INVALID_USERNAME_OR_PASSWORD";
    public const string EXPIRED_REFRESH_TOKEN = "EXPIRED_REFRESH_TOKEN";
}