using ApplicationCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.DTOs;

namespace API;

[Authorize]
public class SettingsController(
    ISettingService settingService
) : DefaultOwnerController
{
    private readonly ISettingService _settingService = settingService;

    [HttpGet("get-owner-blocked-customers")]
    public async Task<ActionResult<ICollection<CustomerCardDto>>> GetOwnerBlockedCustomers()
    {
        var response = await _settingService.GetOwnerBlockedCustomers();
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

}
