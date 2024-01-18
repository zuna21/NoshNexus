using ApplicationCore;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

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
    public async Task<ActionResult<ICollection<OrderCardDto>>> GetInProgressOrders([FromQuery] OwnerQueryParams.OrdersQueryParams ordersQueryParams)
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
    public async Task<ActionResult<PagedList<OrderCardDto>>> GetOrdersHistory([FromQuery] OwnerQueryParams.OrdersHistoryQueryParams ordersHistoryQueryParams)
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

    [HttpGet("block-customer/{orderId}")]
    public async Task<ActionResult<int>> BlockCustomer(int orderId)
    {
        var response = await _orderService.BlockCustomer(orderId);
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

    [HttpGet("accept-order/{orderId}")]
    public async Task<ActionResult<int>> AcceptOrder(int orderId)
    {
        var response = await _orderService.AcceptOrder(orderId);
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

    [HttpPut("decline-order/{orderId}")]
    public async Task<ActionResult<int>> DeclineOrder(int orderId, DeclineReasonDto declineReasonDto)
    {
        var response = await _orderService.DeclineOrder(orderId, declineReasonDto);
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
