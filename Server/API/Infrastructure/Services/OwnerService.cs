using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;

namespace API;

public class OwnerService : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;
    private readonly ICountryRepository _countryRepository;
    private readonly IAppUserImageService _appUserImageService;
    public OwnerService(
        IOwnerRepository ownerRepository,
        UserManager<AppUser> userManager,
        ITokenService tokenService,
        IUserService userService,
        ICountryRepository countryRepository,
        IAppUserImageService appUserImageService
    )
    {
        _ownerRepository = ownerRepository;
        _userManager = userManager;
        _tokenService = tokenService;
        _userService = userService;
        _countryRepository = countryRepository;
        _appUserImageService = appUserImageService;
    }

    public async Task<Response<GetOwnerDto>> GetOwnerDetails()
    {
        Response<GetOwnerDto> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var ownerDetails = await _ownerRepository.GetOwnerDetails(owner.UniqueUsername);
            if (ownerDetails == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }


            response.Status = ResponseStatus.Success;
            response.Data = ownerDetails;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<GetOwnerEditDto>> GetOwnerEdit()
    {
        Response<GetOwnerEditDto> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var ownerEdit = await _ownerRepository.GetOwnerEdit(owner.UniqueUsername);
            if (ownerEdit == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var countries = await _countryRepository.GetAllCountries();
            if (countries == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            ownerEdit.AllCountries = countries;

            response.Status = ResponseStatus.Success;
            response.Data = ownerEdit;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
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
                response.Status = ResponseStatus.NotFound;
                return response;
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

            var user = new AppUser
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

            var country = await _countryRepository.GetCountryById(registerOwnerDto.CountryId);
            if (country == null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to get country.";
                return response;
            }

            var owner = new Owner
            {
                AppUserId = user.Id,
                AppUser = user,
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

    public async Task<Response<int>> Update(EditOwnerDto editOwnerDto)
    {
        Response<int> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var user = await _userManager.FindByNameAsync(owner.UniqueUsername);
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            if (editOwnerDto.Username.ToLower() != user.UserName)
            {
                var usernameTaken = await _userManager.FindByNameAsync(editOwnerDto.Username.ToLower());
                if (usernameTaken != null)
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Username is taken.";
                    return response;
                }

                user.UserName = editOwnerDto.Username.ToLower();
            }

            user.PhoneNumber = editOwnerDto.PhoneNumber;
            user.Email = editOwnerDto.Email;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to update user.";
                return response;
            }

            owner.UniqueUsername = user.UserName;
            owner.Birth = editOwnerDto.Birth;
            owner.FirstName = editOwnerDto.FirstName;
            owner.LastName = editOwnerDto.LastName;
            owner.City = editOwnerDto.City;
            owner.Address = editOwnerDto.Address;
            owner.Description = editOwnerDto.Description;
            if (owner.CountryId != editOwnerDto.CountryId)
            {
                var country = await _countryRepository.GetCountryById(editOwnerDto.CountryId);
                if (country == null)
                {
                    response.Status = ResponseStatus.NotFound;
                    return response;
                }

                owner.CountryId = country.Id;
                owner.Country = country;
            }

            // Ne hendlas Errore (Pazi)
            await _ownerRepository.SaveAllAsync();

            response.Status = ResponseStatus.Success;
            response.Data = owner.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ImageDto>> UploadProfileImage(IFormFile image)
    {
        Response<ImageDto> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var profileImage = await _appUserImageService.UploadProfileImage(user.Id, image);
            if (profileImage == null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to upload profile image.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = profileImage;

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
