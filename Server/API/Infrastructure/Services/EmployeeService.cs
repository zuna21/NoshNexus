﻿using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using EmployeeDtos = ApplicationCore.DTOs.EmployeeDtos;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;
using ApplicationCore.DTOs.EmployeeDtos;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API;

public class EmployeeService(
    IEmployeeRepository employeeRepository,
    UserManager<AppUser> userManager,
    ITokenService tokenService,
    IUserService userService,
    IRestaurantRepository restaurantRepository,
    IAppUserImageRepository appUserImageRepository,
    ICountryRepository countryRepository
    ) : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository = employeeRepository;
    private readonly IRestaurantRepository _restaurantRepository = restaurantRepository;
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IUserService _userService = userService;
    private readonly IAppUserImageRepository _appUserImageRepository = appUserImageRepository;
    private readonly ICountryRepository _countryRepository = countryRepository;

    public async Task<Response<int>> Create(OwnerDtos.CreateEmployeeDto createEmployeeDto)
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
            var restaurant = await _restaurantRepository.GetOwnerRestaurant(createEmployeeDto.RestaurantId, owner.Id);
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

            var user = new AppUser
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
                AppUser = user,
                AppUserId = user.Id,
                LastName = createEmployeeDto.LastName,
                Restaurant = restaurant,
                RestaurantId = restaurant.Id,
                UniqueUsername = user.UserName,
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
            response.Data = employee.Id;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<int>> Delete(int employeeId)
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

            var employee = await _employeeRepository.GetOwnerEmployee(employeeId, owner.Id);
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            employee.IsDeleted = true;
            if (!await _employeeRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete employee.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = employee.Id;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<EmployeeDtos.AccountDto>> EditAccount(EditAccountDto editAccountDto)
    {
        Response<EmployeeDtos.AccountDto> response = new();
        try
        {
            var user = await _userService.GetUser();
            var employee = await _userService.GetEmployee();
            if (user == null || employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            // Za user
            if (
                !string.Equals(user.Email, editAccountDto.Email) || 
                !string.Equals(user.PhoneNumber, editAccountDto.PhoneNumber)
            )
            {
                user.Email = editAccountDto.Email;
                user.PhoneNumber = editAccountDto.PhoneNumber;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Failed to update user account.";
                    return response;
                }
            }

            // za employee
            if (
                !string.Equals(employee.FirstName, editAccountDto.FirstName) ||
                !string.Equals(employee.LastName, editAccountDto.LastName) ||
                !string.Equals(employee.City, editAccountDto.City) ||
                employee.CountryId != editAccountDto.CountryId ||
                !string.Equals(employee.Address, editAccountDto.Address) ||
                !string.Equals(employee.Description, editAccountDto.Description) ||
                !DateTime.Equals(employee.Birth, editAccountDto.Birth)
            )
            {
                if (employee.CountryId != editAccountDto.CountryId)
                {
                    var country = await _countryRepository.GetCountryById(editAccountDto.CountryId);
                    if (country == null)
                    {
                        response.Status = ResponseStatus.NotFound;
                        return response;
                    }
                    employee.Country = country;
                    employee.CountryId = country.Id;
                }

                employee.FirstName = editAccountDto.FirstName;
                employee.LastName = editAccountDto.LastName;
                employee.City = editAccountDto.City;
                employee.Address = editAccountDto.Address;
                employee.Description = editAccountDto.Description;
                employee.Birth = editAccountDto.Birth;

                if (!await _employeeRepository.SaveAllAsync())
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Failed to update employee account.";
                    return response;
                }
            }

            if (
                !string.Equals(employee.UniqueUsername.ToLower(), editAccountDto.Username.ToLower())
            )
            {
                var userExists = await _userManager.FindByNameAsync(editAccountDto.Username.ToLower());
                if (userExists != null)
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Username is taken.";
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

                employee.UniqueUsername = user.UserName;
                if (!await _employeeRepository.SaveAllAsync())
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
                Token = _tokenService.CreateToken(user, "employee"),
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
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = await _employeeRepository.GetAccountDetails(employee.Id);
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
            var employee = await _userService.GetEmployee();
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var account = await _employeeRepository.GetAccountEdit(employee.Id);
            if (account == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }
            var countries = await _countryRepository.GetAllCountries();
            account.AllCountries = countries;

            response.Status = ResponseStatus.Success;
            response.Data = account;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }
        
        return response;
    }

    public async Task<Response<CustomerDtos.EmployeeDto>> GetCustomerEmployee(int employeeId)
    {
        Response<CustomerDtos.EmployeeDto> response = new();
        try
        {
            var employee = await _employeeRepository.GetCustomerEmployee(employeeId);
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }
            
            response.Status = ResponseStatus.Success;
            response.Data = employee;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }
        
        return response;
    }

    public async Task<Response<ICollection<CustomerDtos.EmployeeCardDto>>> GetCustomerEmployees(int restaurantId)
    {
        Response<ICollection<CustomerDtos.EmployeeCardDto>> response = new();
        try
        {
            response.Status = ResponseStatus.Success;
            response.Data = await _employeeRepository.GetCustomerEmployees(restaurantId);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<OwnerDtos.GetEmployeeDetailsDto>> GetEmployee(int id)
    {
        Response<OwnerDtos.GetEmployeeDetailsDto> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var employee = await _employeeRepository.GetEmployee(id, owner.Id);
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = employee;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }


    public async Task<Response<OwnerDtos.GetEmployeeEditDto>> GetEmployeeEdit(int id)
    {
        Response<OwnerDtos.GetEmployeeEditDto> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }


            var employee = await _employeeRepository.GetEmployeeEdit(id, owner.Id);
            if (employee == null) 
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            /* var ownerRestaurants = await _restaurantService.GetRestaurantsForSelect();
            if (ownerRestaurants == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            } */

            response.Status = ResponseStatus.Success;
            response.Data = employee;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

    public async Task<Response<PagedList<OwnerDtos.EmployeeCardDto>>> GetEmployees(OwnerQueryParams.EmployeesQueryParams employeesQueryParams)
    {
        Response<PagedList<OwnerDtos.EmployeeCardDto>> response = new();
        try
        {
            var owner = await _userService.GetOwner();
            if (owner == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var employees = await _employeeRepository.GetEmployees(owner.Id, employeesQueryParams);
            response.Status = ResponseStatus.Success;
            response.Data = employees;
        }
        catch(Exception ex)
        {
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong";
            Console.WriteLine(ex.ToString());
        }

        return response;
    }

 

    public async Task<Response<EmployeeDtos.AccountDto>> Login(EmployeeDtos.LoginDto loginEmployeeDto)
    {
        Response<EmployeeDtos.AccountDto> response = new();
        try
        {
            var user = await _userManager.FindByNameAsync(loginEmployeeDto.Username.ToLower());
            if (user == null)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = "Invalid username or password.";
                return response;
            }

            var result = await _userManager.CheckPasswordAsync(user, loginEmployeeDto.Password);
            if (!result)
            {
                response.Status = ResponseStatus.Unauthorized;
                response.Message = "Invalid username or password.";
                return response;
            }


            response.Status = ResponseStatus.Success;
            response.Data = new EmployeeDtos.AccountDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user, "employee"),
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

    public async Task<Response<int>> Update(int employeeId, OwnerDtos.EditEmployeeDto editEmployeeDto)
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

            var employee = await _employeeRepository.GetOwnerEmployee(employeeId, owner.Id);
            if (employee == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var user = await _userManager.FindByNameAsync(employee.UniqueUsername);
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            if (employee.RestaurantId != editEmployeeDto.RestaurantId)
            {
                var restaurant = await _restaurantRepository.GetOwnerRestaurant(editEmployeeDto.RestaurantId, owner.Id);
                if (restaurant == null)
                {
                    response.Status = ResponseStatus.NotFound;
                    return response;
                }

                employee.RestaurantId = restaurant.Id;
                employee.Restaurant = restaurant;
            }

            if (user.UserName != editEmployeeDto.Username.ToLower())
            {
                var userExists = await _userManager.FindByNameAsync(editEmployeeDto.Username.ToLower());
                if (userExists != null)
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Username is taken.";
                    return response;
                }

                user.UserName = editEmployeeDto.Username.ToLower();
            }

            user.Email = editEmployeeDto.Email;
            user.PhoneNumber = editEmployeeDto.PhoneNumber;
            var userUpdated = await _userManager.UpdateAsync(user);
            if (!userUpdated.Succeeded)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to update user.";
                return response;
            }

            // Jos uvijek se password ne moze promijeniti jer mora unijeti i stari password
            // ....

            employee.FirstName = editEmployeeDto.FirstName;
            employee.LastName = editEmployeeDto.LastName;
            employee.Address = editEmployeeDto.Address;
            employee.CanEditMenus = editEmployeeDto.CanEditMenus;
            employee.CanEditFolders = editEmployeeDto.CanEditFolders;
            employee.CanViewFolders = editEmployeeDto.CanViewFolders;
            employee.Birth = editEmployeeDto.Birth;
            employee.Description = editEmployeeDto.Description;
            employee.City = editEmployeeDto.City;
            employee.UniqueUsername = user.UserName;

            if (!await _employeeRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to update employee.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = employee.Id;
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
