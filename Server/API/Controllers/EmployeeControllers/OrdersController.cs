using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.EmployeeControllers;

[Authorize]
public class OrdersController(IOrderService orderService) : DefaultEmployeeController
{
    private readonly IOrderService _orderService = orderService;

    [HttpGet("get-in-progress-orders")]
    public async Task<ActionResult<ICollection<OrderCardDto>>> GetInProgressOrders()
    {
        var response = await _orderService.GetEmployeeInProgressOrders();
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
