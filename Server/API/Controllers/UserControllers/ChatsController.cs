﻿using ApplicationCore;
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
}
