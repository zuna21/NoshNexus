using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.EmployeeControllers;


public class AccountController : DefaultEmployeeController
{
    private readonly IEmployeeService _employeeService;
    public AccountController(
        IEmployeeService employeeService
    )
    {
        _employeeService = employeeService;
    }

    [HttpPost("login")]
    public async Task<ActionResult<EmployeeAccountDto>> Login(LoginEmployeeDto loginEmployeeDto)
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
