using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<bool>> Create(int id, CreateOrderDto createOrderDto)
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
}
