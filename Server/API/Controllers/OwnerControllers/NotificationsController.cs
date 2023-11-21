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

    [HttpPut("mark-notification-as-read/{id}")]
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

}
