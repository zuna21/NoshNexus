using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace API;

[Authorize]
public class EmployeesController(
    IEmployeeService employeeService,
    IAppUserImageService appUserImageService
    ) : DefaultOwnerController
{
    private readonly IEmployeeService _employeeService = employeeService;
    private readonly IAppUserImageService _appUserImageService = appUserImageService;

    [HttpPost("create")]
    public async Task<ActionResult<int>> Create(OwnerDtos.CreateEmployeeDto createEmployeeDto)
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
    public async Task<ActionResult<int>> Update(int id, OwnerDtos.EditEmployeeDto editEmployeeDto)
    {
        var response = await _employeeService.Update(id, editEmployeeDto);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<int>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<int>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<int>)response.Data,
            _ => (ActionResult<int>)BadRequest("Something went wrong."),
        };
    }

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<int>> Delete(int id)
    {
        var response = await _employeeService.Delete(id);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<int>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<int>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<int>)response.Data,
            _ => (ActionResult<int>)BadRequest("Something went wrong."),
        };
    }

    [HttpGet("get-employees")]
    public async Task<ActionResult<ICollection<OwnerDtos.EmployeeCardDto>>> GetEmployees([FromQuery] OwnerQueryParams.EmployeesQueryParams employeesQueryParams)
    {
        var response = await _employeeService.GetEmployees(employeesQueryParams);
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
    public async Task<ActionResult<OwnerDtos.GetEmployeeEditDto>> GetEmployeeEdit(int id)
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
    public async Task<ActionResult<OwnerDtos.GetEmployeeDetailsDto>> GetEmployee(int id)
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

    [HttpPost("upload-profile-image/{employeeId}")]
    public async Task<ActionResult<ImageDto>> UploadProfileImage(int employeeId)
    {
        var image = Request.Form.Files[0];
        var response = await _appUserImageService.UploadEmployeeProfileImage(employeeId, image);
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
