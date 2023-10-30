
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace API;

public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ICountryService _countryService;
    public OwnerService(
        IOwnerRepository ownerRepository,
        UserManager<IdentityUser> userManager,
        ITokenService tokenService,
        IHttpContextAccessor httpContextAccessor,
        ICountryService countryService
    )
    {
        _ownerRepository = ownerRepository;
        _userManager = userManager;
        _tokenService = tokenService;
        _httpContextAccessor = httpContextAccessor;
        _countryService = countryService;
    }

    public async Task<Owner> GetOwner()
    {
        var username =_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return await _ownerRepository.GetOwnerByUsername(username);
    }

    public async Task<Response<OwnerAccountDto>> Login(LoginOwnerDto loginOwnerDto)
    {
        Response<OwnerAccountDto> response = new();
        try
        {
            var user = await _userManager.FindByNameAsync(loginOwnerDto.Username.ToLower());
            if (user == null)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = "Invalid username or password.";
                return response;
            }

            var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginOwnerDto.Password);
            if (!isPasswordCorrect)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = "Invalid username or password.";
                return response;
            }

            var doesOwnerExists = await _ownerRepository.DoesOwnerExists(user.UserName);
            if (!doesOwnerExists)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = "You have no permissions.";
            }

            response.Status = ResponseStatus.Success;
            response.Message = "Successfully logged in.";
            response.Data = new OwnerAccountDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }
        catch (Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<OwnerAccountDto>> Register(RegisterOwnerDto registerOwnerDto)
    {
        Response<OwnerAccountDto> response = new();
        try
        {
            var userExists = await _userManager.FindByNameAsync(registerOwnerDto.Username.ToLower());
            if (userExists != null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Username is taken.";
                return response;
            }

            var user = new IdentityUser
            {
                UserName = registerOwnerDto.Username.ToLower(),
                Email = registerOwnerDto.Email,
                PhoneNumber = registerOwnerDto.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, registerOwnerDto.Password);
            if (!result.Succeeded)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create user.";
                return response;
            }

            var country = await _countryService.GetCountryById(registerOwnerDto.CountryId);
            if (country == null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to get country.";
                return response;
            }

            var owner = new Owner
            {
                IdentityUserId = user.Id,
                IdentityUser = user,
                UniqueUsername = user.UserName,
                Country = country,
                CountryId = country.Id
            };

            _ownerRepository.Create(owner);
            if (!await _ownerRepository.SaveAllAsync())
            {
                await _userManager.DeleteAsync(user);
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create an account.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Message = "Successfully created an account.";
            response.Data = new OwnerAccountDto
            {
                Username = owner.UniqueUsername,
                Token = _tokenService.CreateToken(user)
            };
        }
        catch (Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }
}
