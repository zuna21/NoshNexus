using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class EmployeesController : DefaultOwnerController
{
    private readonly IEmployeeService _employeeService;
    public EmployeesController(
        IEmployeeService employeeService
    )
    {
        _employeeService = employeeService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create(CreateEmployeeDto createEmployeeDto)
    {
        var result = await _employeeService.Create(createEmployeeDto);
        switch (result.Status)
        {
            case ResponseStatus.UsernameTaken:
                return BadRequest(result.Message);
            case ResponseStatus.BadRequest:
                return BadRequest(result.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return Ok(result.Data);
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpGet("get-employees")]
    public async Task<ActionResult<ICollection<EmployeeCardDto>>> GetEmployees()
    {
        var response = await _employeeService.GetEmployees();
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpGet("get-employee-edit/{id}")]
    public async Task<ActionResult<GetEmployeeEditDto>> GetEmployeeEdit(int id)
    {
        var response = await _employeeService.GetEmployeeEdit(id);
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong");
        }
    }

    [HttpGet("get-employee-details/{id}")]
    public async Task<ActionResult<EmployeeDetailsDto>> GetEmployeeDetails(int id)
    {
        var response = await _employeeService.GetEmployeeDetails(id);
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong");
        }
    }
}
