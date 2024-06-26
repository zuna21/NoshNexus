﻿using ApplicationCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApplicationCore.DTOs;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace API;

[Authorize]
public class ChartsController(
    IChartService chartService
) : DefaultOwnerController
{
    private readonly IChartService _chartService = chartService;

    [HttpGet("get-orders-by-day/{restaurantId}")]
    public async Task<ActionResult<ICollection<int>>> GetOrdersByDay(int restaurantId, [FromQuery] OwnerQueryParams.OrdersByDayQueryParams ordersByDayQueryParams)
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

    [HttpGet("get-top-ten-menu-items/{restaurantId}")]
    public async Task<ActionResult<OwnerDtos.PieChartDto>> GetTopTenMenuItems(int restaurantId, [FromQuery] OwnerQueryParams.TopTenMenuOrdersQueryParams topTenMenuOrdersQueryParams)
    {
        var response = await _chartService.GetTopTenMenuItems(restaurantId, topTenMenuOrdersQueryParams);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpGet("get-orders-by-hour/{restaurantId}")]
    public async Task<ActionResult<OwnerDtos.LineChartDto>> GetOrdersByHour(int restaurantId, [FromQuery] OwnerQueryParams.OrdersByHourQueryParams ordersByHourQueryParams)
    {
        var response = await _chartService.GetOrdersByHour(restaurantId, ordersByHourQueryParams);
        switch (response.Status)
        {
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }

}
