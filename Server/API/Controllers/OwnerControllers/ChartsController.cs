using ApplicationCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.DTOs;

namespace API;

[Authorize]
public class ChartsController(
    IChartService chartService
) : DefaultOwnerController
{
    private readonly IChartService _chartService = chartService;

    [HttpGet("get-week-day-orders/{restaurantId}")]
    public async Task<ActionResult<ICollection<WeekDayOrdersDto>>> GetWeekDayOrders(int restaurantId)
    {
        var response = await _chartService.GetWeekDayOrders(restaurantId);
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

    [HttpGet("get-top-ten-menu-items/{restaurantId}")]
    public async Task<ActionResult<ICollection<TopTenMenuItemsDto>>> GetTopTenMenuItems(int restaurantId)
    {
        var response = await _chartService.GetTopTenMenuItems(restaurantId);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return Ok(response.Data);
            default:
                return BadRequest("Something went wrong.");
        }
    }
}
