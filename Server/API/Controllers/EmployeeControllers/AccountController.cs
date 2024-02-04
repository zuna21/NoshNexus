using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using EmployeeDtos = ApplicationCore.DTOs.EmployeeDtos;

namespace API.Controllers.EmployeeControllers;


public class AccountController(
    IEmployeeService employeeService,
    IUserService userService,
    ITokenService tokenService,
    IAppUserImageRepository appUserImageRepository
    ) : DefaultEmployeeController
{
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IUserService _userService = userService;
    private readonly ITokenService _tokenService = tokenService;
    private readonly IAppUserImageRepository _appUserImageRepository = appUserImageRepository;

    [HttpPost("login")]
    public async Task<ActionResult<EmployeeDtos.AccountDto>> Login(EmployeeDtos.LoginDto loginEmployeeDto)
    {
        var response = await _employeeService.Login(loginEmployeeDto);
        switch (response.Status)
        {
            case ResponseStatus.Unauthorized:
                return Unauthorized(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }
}
