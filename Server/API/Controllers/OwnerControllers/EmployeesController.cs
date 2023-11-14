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
    public async Task<ActionResult<int>> Create(CreateEmployeeDto createEmployeeDto)
    {
        var resposne = await _employeeService.Create(createEmployeeDto);
        switch (resposne.Status)
        {
            case ResponseStatus.UsernameTaken:
                return BadRequest(resposne.Message);
            case ResponseStatus.BadRequest:
                return BadRequest(resposne.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return Ok(resposne.Data);
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpPut("update/{id}")]
    public async Task<ActionResult<int>> Update(int id, EditEmployeeDto editEmployeeDto)
    {
        var response = await _employeeService.Update(id, editEmployeeDto);
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

    [HttpGet("get-employee/{id}")]
    public async Task<ActionResult<EmployeeDetailsDto>> GetEmployee(int id)
    {
        var response = await _employeeService.GetEmployee(id);
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
