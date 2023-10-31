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
}
