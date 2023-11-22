using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class ChatsController : DefaultOwnerController
{
    private readonly IChatService _chatService;
    public ChatsController(
        IChatService chatService
    )
    {
        _chatService = chatService;
    }


    [HttpGet("get-users-for-chat-participants")]
    public async Task<ActionResult<List<ChatParticipantDto>>> GetUsersForChatParticipants(string sq)
    {
        var response = await _chatService.GetUsersForChatParticipants(sq);
        return response.Status switch
        {
            ResponseStatus.NotFound => (ActionResult<List<ChatParticipantDto>>)NotFound(),
            ResponseStatus.BadRequest => (ActionResult<List<ChatParticipantDto>>)BadRequest(response.Message),
            ResponseStatus.Success => (ActionResult<List<ChatParticipantDto>>)response.Data,
            _ => (ActionResult<List<ChatParticipantDto>>)BadRequest(response.Message),
        };
    }
}
