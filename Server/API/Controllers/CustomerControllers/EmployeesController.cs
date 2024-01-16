using ApplicationCore.Contracts.ServicesContracts;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.DTOs;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

namespace API.Controllers.CustomerControllers;

public class EmployeesController(
    IEmployeeService employeeService
) : DefaultCustomerController
{
    private readonly IEmployeeService _employeeService = employeeService;

    [HttpGet("get-employees/{restaurantId}")]
    public async Task<ActionResult<CustomerDtos.EmployeeCardDto>> GetEmployees(int restaurantId)
    {
        var response = await _employeeService.GetCustomerEmployees(restaurantId);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Someting went wrong.");
        }
    }

    [HttpGet("get-employee/{employeeId}")]
    public async Task<ActionResult<CustomerDtos.EmployeeDto>> GetEmployee(int employeeId)
    {
        var response = await _employeeService.GetCustomerEmployee(employeeId);
        switch (response.Status)
        {
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
