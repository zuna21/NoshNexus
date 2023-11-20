using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class OwnersController : DefaultOwnerController
{
    private readonly IOwnerService _ownerService;
    public OwnersController(
        IOwnerService ownerService
    )
    {
        _ownerService = ownerService;
    }

    [HttpGet("get-owner-edit")]
    public async Task<ActionResult<GetOwnerEditDto>> GetOwnerEdit()
    {
        var response = await _ownerService.GetOwnerEdit();
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


    [HttpPut("update")]
    public async Task<ActionResult<int>> Update(EditOwnerDto editOwnerDto)
    {
        var response = await _ownerService.Update(editOwnerDto);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<int>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<int>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<int>)response.Data,
            _ => (ActionResult<int>)BadRequest("Something went wrong."),
        };
    }
}
