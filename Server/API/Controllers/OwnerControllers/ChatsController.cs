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

    [HttpPost("create-chat")]
    public async Task<ActionResult<bool>> CreateChat(CreateChatDto createChatDto)
    {
        var response = await _chatService.CreateChat(createChatDto);
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.BadRequest:
                return BadRequest(response.Status);
            case ResponseStatus.Success:
                return response.Data;
            default:
                return BadRequest("Something went wrong.");
        }
    }
}
