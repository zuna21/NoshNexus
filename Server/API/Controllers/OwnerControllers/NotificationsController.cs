using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class NotificationsController : DefaultOwnerController
{
    private readonly INotificationService _notificationService;
    public NotificationsController(
        INotificationService notificationService
    )
    {
        _notificationService = notificationService;
    }

    [HttpPost("create-for-all-users")]
    public async Task<ActionResult<bool>> CreateForAllUsers(CreateNotificationDto createNotificationDto)
    {
        var response = await _notificationService.CreateNotificationForAllUsers(createNotificationDto);
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

    [HttpGet("get-notifications-for-menu")]
    public async Task<ActionResult<GetNotificationForMenuDto>> GetNotificationsForMenu()
    {
        var response = await _notificationService.GetNotificationForMenu(5);
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

    [HttpGet("mark-notification-as-read/{id}")]
    public async Task<ActionResult<int>> MarkNotificationAsRead(int id)
    {
        var response = await _notificationService.MarkNotificationAsRead(id);
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


    [HttpGet("mark-all-notifications-as-read")]
    public async Task<ActionResult<bool>> MarkAllNotificationsAsRead()
    {
        var response = await _notificationService.MarkAllNotificationsAsRead();
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<bool>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<bool>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<bool>)response.Data,
            _ => (ActionResult<bool>)BadRequest("Something went wrong"),
        };
    }

    [HttpGet("get-all-notifications")]
    public async Task<ActionResult<List<GetNotificationDto>>> GetAllNotifications()
    {
        var response = await _notificationService.GetAllNotifications();
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<List<GetNotificationDto>>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<List<GetNotificationDto>>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<List<GetNotificationDto>>)response.Data,
            _ => (ActionResult<List<GetNotificationDto>>)BadRequest("Something went wrong."),
        };
    }

}
