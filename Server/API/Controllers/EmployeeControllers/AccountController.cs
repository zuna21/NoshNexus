using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using EmployeeDtos = ApplicationCore.DTOs.EmployeeDtos;

namespace API.Controllers.EmployeeControllers;


public class AccountController : DefaultEmployeeController
{
    private readonly IEmployeeService _employeeService;
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    public AccountController(
        IEmployeeService employeeService,
        IUserService userService,
        ITokenService tokenService
    )
    {
        _employeeService = employeeService;
        _userService = userService;
        _tokenService = tokenService;
    }

    [Authorize]
    [HttpGet("get-user")]
    public async Task<ActionResult<EmployeeDtos.AccountDto>> GetUser()
    {
        var user = await _userService.GetUser();
        if (user == null)
        {
            return NotFound();
        }

        return new EmployeeDtos.AccountDto
        {
            Username = user.UserName,
            Token = _tokenService.CreateToken(user)
        };
    }

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
