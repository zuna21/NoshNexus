﻿using System.Security.Claims;
using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.SignalR;

namespace API;

public class ChatHub : Hub
{
        private readonly IAppUserRepository _appUserRepository;
        private readonly IChatRepository _chatRepository;
        public ChatHub(
            IAppUserRepository appUserRepository,
            IChatRepository chatRepository
        )
        {
            _appUserRepository = appUserRepository;
            _chatRepository = chatRepository;
        }

    public async Task JoinGroups()
    {
        var username = Context.UserIdentifier;
        AppUser user = _appUserRepository.GetUserByUsernameSync(username);
        if (user == null)
        {
            return;
        }

        var chats = _chatRepository.GetUserChatUniqueNamesSync(user.Id);
        foreach (var chat in chats)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chat);
        }
    }


    public async Task SendMessage(int chatId, CreateMessageDto createMessageDto)
    {
        var username = Context.UserIdentifier;
        var user = _appUserRepository.GetUserByUsernameSync(username);
        if (user == null) return;
        var chat = _chatRepository.GetUserChatSync(chatId, user.Id);
        if (chat == null) return;
        var message = new Message
        {
            Content = createMessageDto.Content,
            AppUserId = user.Id,
            ChatId = chat.Id,
            Chat = chat,
            Sender = user
        };

        var chatAppUsersRelations = _chatRepository.GetChatAppUsersRelationsSync(chat.Id);
        foreach (var chatAppUserRelation in chatAppUsersRelations)
        {
            if (chatAppUserRelation.AppUserId == user.Id) chatAppUserRelation.IsSeen = true;
            chatAppUserRelation.IsSeen = false;
        }

        _chatRepository.CreateMessage(message);
        _chatRepository.SaveAllSync();

        MessageDto messageDto = new()
        {
            Id = message.Id,
            Content = message.Content,
            IsMine = true,
            CreatedAt = message.CreatedAt,
            Sender = new ChatSenderDto
            {
                Id = user.Id,
                IsActive = user.IsActive,
                Username = user.UserName,
                ProfileImage = ""
            }
        };

        await Clients.Caller.SendAsync("ReceiveMyMessage", messageDto);

        messageDto.IsMine = false;

        await Clients.GroupExcept(chat.UniqueName, Context.ConnectionId).SendAsync("ReceiveMessage", messageDto);

    }

    public async Task LeaveGroup(string groupName)
        {
            // Remove the current user from a group
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
}
