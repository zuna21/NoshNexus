using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.EmployeeControllers;

[Authorize]
public class Tables : DefaultEmployeeController
{
    private readonly ITableService _tableService;
    public Tables(
        ITableService tableService
    )
    {
        _tableService = tableService;
    }

    [HttpPost("create")]
    public async Task<ActionResult<bool>> Create(ICollection<TableCardDto> tableCardDtos)
    {
        var response = await _tableService.EmployeeCreate(tableCardDtos);
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

    [HttpDelete("delete/{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var response = await _tableService.EmployeeDelete(id);
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

    [HttpGet("get-tables")]
    public async Task<ActionResult<ICollection<TableCardDto>>> GetTables()
    {
        var response = await _tableService.GetEmployeeTables();
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong");
        }
    }
}
