using ApplicationCore;
using ApplicationCore.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API;

[Authorize]
public class ChatsController(
    IChatService chatService
) : DefaultUserController
{
    private readonly IChatService _chatService = chatService;

    [HttpPost("create-chat")]
    public async Task<ActionResult<ChatDto>> CreateChat(CreateChatDto createChatDto)
    {
        var response = await _chatService.Create(createChatDto);
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

    [HttpGet("get-users-for-chat-participants")]
    public async Task<ActionResult<ICollection<ChatParticipantDto>>> GetUsersForChatParticipants(string sq="")
    {
        var response = await _chatService.GetUsersForChatParticipants(sq);
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

    [HttpGet("get-chat/{chatId}")]
    public async Task<ActionResult<ChatDto>> GetChat(int chatId)
    {
        var response = await _chatService.GetChat(chatId);
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

    [HttpGet("get-chats")]
    public async Task<ActionResult<ICollection<ChatPreviewDto>>> GetChats(string sq = "")
    {
        var response = await _chatService.GetChats(sq);
        switch (response.Status)
        {
            case ResponseStatus.NotFound:
                return NotFound();
            case ResponseStatus.Success:
                return Ok(response.Data);
            case ResponseStatus.BadRequest:
                return BadRequest(response.Message);
            default:
                return BadRequest("Something went wrong.");
        }
    }

    [HttpGet("get-chats-for-menu")]
    public async Task<ActionResult<ChatMenuDto>> GetChatsForMenu()
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

    [HttpGet("mark-all-as-read")]
    public async Task<ActionResult<bool>> MarkAllAsRead()
    {
        var response = await _chatService.MarkAllAsRead();
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

    [HttpDelete("remove-participant/{chatId}/{participantId}")]
    public async Task<ActionResult<int>> RemoveParticipant(int chatId, int participantId)
    {
        var response = await _chatService.RemoveParticipant(participantId, chatId);
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

    [HttpDelete("delete-chat/{chatId}")]
    public async Task<ActionResult<int>> DeleteChat(int chatId)
    {
        var response = await _chatService.DeleteChat(chatId);
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

    [HttpPut("update/{chatId}")]
    public async Task<ActionResult<ChatDto>> Update(int chatId, CreateChatDto createChatDto)
    {
        var response = await _chatService.Update(chatId, createChatDto);
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
