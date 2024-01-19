using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

namespace API;

[Authorize]
public class OrdersController : DefaultCustomerController
{
    private readonly IOrderService _orderService;
    public OrdersController(
        IOrderService orderService
    )
    {
        _orderService = orderService;
    }


    [HttpPost("create/{id}")]
    public async Task<ActionResult<bool>> Create(int id, CustomerDtos.CreateOrderDto createOrderDto)
    {
        var response = await _orderService.CreateOrder(id ,createOrderDto);
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

    [HttpGet("get-in-progress-orders/{restaurantId}")]
    public async Task<ActionResult<CustomerLiveRestaurantOrdersDto>> GetInProgressOrders(int restaurantId)
    {
        var response = await _orderService.GetCustomerInProgressOrders(restaurantId);
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound(response.Message);
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpGet("get-orders")]
    public async Task<ActionResult<ICollection<OrderCardDto>>> GetOrders(string sq = "")
    {
        var response = await _orderService.GetCustomerOrders(sq);
        switch (response.Status)
        {
            case ResponseStatus.Success:
                return Ok(response.Data);
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            case ResponseStatus.NotFound:
                return NotFound();
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpGet("get-accepted-orders")]
    public async Task<ActionResult<ICollection<OrderCardDto>>> GetAcceptedOrders(string sq = "")
    {
        var response = await _orderService.GetCustomerAcceptedOrders(sq);
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

    [HttpGet("get-declined-orders")]
    public async Task<ActionResult<ICollection<OrderCardDto>>> GetDeclinedOrders(string sq = "")
    {
        var response = await _orderService.GetCustomerDeclinedOrders(sq);
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
