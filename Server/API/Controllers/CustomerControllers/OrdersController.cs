using ApplicationCore;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using FirebaseAdmin.Messaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using CustomerQueryParams = ApplicationCore.QueryParams.CustomerQueryParams;

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
    public async Task<ActionResult<ICollection<OrderCardDto>>> GetOrders([FromQuery] CustomerQueryParams.OrdersQueryParams ordersQueryParams) 
    {
        var response = await _orderService.GetCustomerOrders(ordersQueryParams);
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

    [HttpPost("send-message")]
    public async Task<ActionResult> SendMessage(FirebaseMessageDto firebaseMessageDto) 
    {
        var message = new Message()
        {
            Notification = new Notification
            {
                Title = firebaseMessageDto.Title,
                Body = firebaseMessageDto.Body
            },
            Token = firebaseMessageDto.DeviceToken
        };

        var messaging = FirebaseMessaging.DefaultInstance;
        var result = await messaging.SendAsync(message);

        if (!string.IsNullOrEmpty(result))
        {
            // Message was sent successfully
            return Ok("Message sent successfully!");
        }
        else
        {
            // There was an error sending the message
            return BadRequest("Failed to send message");
        }
    }


}
