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
}
