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

    [HttpGet("get-orders-by-day/{restaurantId}")]
    public async Task<ActionResult<ICollection<int>>> GetOrdersByDay(int restaurantId, [FromQuery] OrdersByDayQueryParams ordersByDayQueryParams)
    {
        var response = await _chartService.GetOrdersByDay(restaurantId, ordersByDayQueryParams);
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
