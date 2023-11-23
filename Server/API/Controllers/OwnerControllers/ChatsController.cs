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
    public async Task<ActionResult<ChatDto>> CreateChat(CreateChatDto createChatDto)
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

    [HttpGet("get-chats")]
    public async Task<ActionResult<ICollection<ChatPreviewDto>>> GetChats() 
    {
        var response = await _chatService.GetChats();
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

    [HttpPost("create-message/{id}")]
    public async Task<ActionResult<MessageDto>> CreateMessage(int id, CreateMessageDto createMessageDto)
    {
        var response = await _chatService.CreateMessage(id, createMessageDto);
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

    [HttpGet("get-chat/{id}")]
    public async Task<ActionResult<ChatDto>> GetChat(int id)
    {
        var response = await _chatService.GetChat(id);
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

    [HttpGet("get-chats-for-menu")]
    public async Task<ActionResult<ChatMenuDto>> GetChatForMenu()
    {
        var response = await _chatService.GetChatsForMenu();
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
