﻿using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace API;

public class OwnerService(
    IOwnerRepository ownerRepository,
    UserManager<AppUser> userManager,
    ITokenService tokenService,
    IUserService userService,
    ICountryRepository countryRepository,
    IAppUserImageRepository appUserImageRepository
    ) : IOwnerService
{
    private readonly IOwnerRepository _ownerRepository = ownerRepository;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUserService _userService = userService;
    private readonly ICountryRepository _countryRepository = countryRepository;
    private readonly IAppUserImageRepository _appUserImageRepository = appUserImageRepository;

    public async Task<Response<OwnerDtos.GetAccountDetailsDto>> GetOwnerDetails()
    {
        Response<OwnerDtos.GetAccountDetailsDto> response = new();
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

    public async Task<Response<OwnerDtos.GetAccountEditDto>> GetOwnerEdit()
    {
        Response<OwnerDtos.GetAccountEditDto> response = new();
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

    public async Task<Response<OwnerDtos.AccountDto>> Register(OwnerDtos.RegisterDto registerOwnerDto)
    {
        Response<OwnerDtos.AccountDto> response = new();
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
            response.Data = new OwnerDtos.AccountDto
            {
                Username = owner.UniqueUsername,
                Token = _tokenService.CreateToken(user, "owner"),
                ProfileImage = await _appUserImageRepository.GetProfileImageUrl(user.Id)
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

    public async Task<Response<OwnerDtos.AccountDto>> Update(OwnerDtos.EditAccountDto editOwnerDto)
    {
        Response<OwnerDtos.AccountDto> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            var user = await _userService.GetUser();
            if (owner == null || user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            // Update user account
            if (
                !string.Equals(editOwnerDto.Email, user.Email) ||
                !string.Equals(editOwnerDto.PhoneNumber, user.PhoneNumber)
            )
            {
                user.Email = editOwnerDto.Email;
                user.PhoneNumber = editOwnerDto.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Failed to update user account.";
                    return response;
                }
            }

            // Update owner account
            if (
                !string.Equals(editOwnerDto.FirstName, owner.FirstName) ||
                !string.Equals(editOwnerDto.LastName, owner.LastName) ||
                !string.Equals(editOwnerDto.Description, owner.Description) ||
                !string.Equals(editOwnerDto.City, owner.City) ||
                !string.Equals(editOwnerDto.Address, owner.Address) ||
                !DateTime.Equals(editOwnerDto.Birth, owner.Birth) ||
                editOwnerDto.CountryId != owner.CountryId
            )
            {
                if (editOwnerDto.CountryId != owner.CountryId)
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

                owner.FirstName = editOwnerDto.FirstName;
                owner.LastName = editOwnerDto.LastName;
                owner.Description = editOwnerDto.Description;
                owner.City = editOwnerDto.City;
                owner.Address = editOwnerDto.Address;
                owner.Birth = editOwnerDto.Birth;

                if (!await _ownerRepository.SaveAllAsync())
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Failed to update owner account.";
                    return response;
                }
            }

            // update username
            if (
                !string.Equals(user.UserName.ToLower(), editOwnerDto.Username.ToLower())
            )
            {
                var alreadyExist = await _userManager.FindByNameAsync(editOwnerDto.Username.ToLower());
                if (alreadyExist != null)
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Username is already taken.";
                    return response;
                }

                user.UserName = editOwnerDto.Username.ToLower();
                owner.UniqueUsername = editOwnerDto.Username.ToLower();
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Failed to update username.";
                    return response;
                }
            }

            response.Status = ResponseStatus.Success;
            response.Data = new OwnerDtos.AccountDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user, "owner"),
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
