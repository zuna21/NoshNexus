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
    public async Task<ActionResult<PagedList<CustomerCardDto>>> GetOwnerBlockedCustomers([FromQuery] BlockedCustomersQueryParams blockedCustomersQueryParams)
    {
        var response = await _settingService.GetOwnerBlockedCustomers(blockedCustomersQueryParams);
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

    [HttpDelete("unblock-customer/{customerId}")]
    public async Task<ActionResult<int>> UnblockCustomer(int customerId)
    {
        var response = await _settingService.UnblockCustomer(customerId);
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
