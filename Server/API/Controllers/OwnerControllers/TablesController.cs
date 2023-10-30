using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class TablesController : DefaultOwnerController
{
    private readonly ITableService _tableService;
    public TablesController(
        ITableService tableService
    )
    {
        _tableService = tableService;
    }

    [HttpPost("create")]
    public async Task<ActionResult> Create(ICollection<TableCardDto> tableCardDtos)
    {
        var response = await _tableService.CreateTables(tableCardDtos);
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


    [HttpGet("get-tables")]
    public async Task<ActionResult<ICollection<TableCardDto>>> GetTables()
    {
        var response = await _tableService.GetTables();
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
