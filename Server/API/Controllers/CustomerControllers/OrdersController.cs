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

    [HttpGet("get-orders")]
    public async Task<ActionResult<ICollection<OrderCardDto>>> GetOrders() 
    {
        var response = await _orderService.GetCustomerOrders();
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
