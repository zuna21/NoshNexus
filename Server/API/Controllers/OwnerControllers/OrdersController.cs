﻿using ApplicationCore;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.OwnerController;

[Authorize]
public class OrdersController : DefaultOwnerController
{
    private readonly IOrderService _orderService;
    public OrdersController(
        IOrderService orderService
    )
    {
        _orderService = orderService;
    }

    [HttpGet("get-in-progress-orders")]
    public async Task<ActionResult<ICollection<OrderCardDto>>> GetInProgressOrders([FromQuery] OrdersQueryParams ordersQueryParams)
    {
        var response = await _orderService.GetOwnerInProgressOrders(ordersQueryParams);
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

    [HttpGet("get-orders-history")]
    public async Task<ActionResult<ICollection<OrderCardDto>>> GetOrdersHistory([FromQuery] OrdersHistoryQueryParams ordersHistoryQueryParams)
    {
        var response = await _orderService.GetOrdersHistory(ordersHistoryQueryParams);
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
