using System.Net.NetworkInformation;
using ApplicationCore;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace API;

public class ChatRepository(
    DataContext dataContext
) : IChatRepository
{
    private readonly DataContext _context = dataContext;

    public void AddChatConnection(ChatConnection chatConnection)
    {
        _context.ChatConnections.Add(chatConnection);
    }

    public void AddChatParticipants(ICollection<AppUserChat> chatParticipants)
    {
        _context.AppUserChats.AddRange(chatParticipants);
    }

    public void AddMessage(Message message)
    {
        _context.Messages.Add(message);
    }

    public void Create(Chat chat)
    {
        _context.Chats.Add(chat);
    }

    public async Task<ICollection<AppUser>> GetAppUserByIds(ICollection<int> userIds)
    {
        return await _context.Users
            .Where(x => userIds.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<ChatDto> GetChat(int chatId, int userId)
    {
        return await _context.Chats
            .Where(x => x.Id == chatId && x.AppUserChats.Select(uc => uc.AppUserId).Contains(userId))
            .Select(x => new ChatDto
            {
                Id = x.Id,
                Name = x.Name,
                Messages = x.Messages
                    .Select(m => new MessageDto
                    {
                        Id = m.Id,
                        Content = m.Content,
                        CreatedAt = m.CreatedAt,
                        Sender = new ChatSenderDto
                        {
                            Id = m.AppUserId,
                            IsActive = m.Sender.IsActive,
                            ProfileImage = m.Sender.AppUserImages
                                .Where(si => si.IsDeleted == false && si.Type == AppUserImageType.Profile)
                                .Select(si => si.Url)
                                .FirstOrDefault(),
                            Username = m.Sender.UserName
                        },
                        IsMine = m.AppUserId == userId
                    })
                    .ToList(),
                Participants = x.AppUserChats
                    .Where(uc => uc.AppUserId != userId)
                    .Select(uc => new ChatParticipantDto
                    {
                        Id = uc.AppUserId,
                        ProfileImage = uc.AppUser.AppUserImages
                            .Where(pi => pi.IsDeleted == false && pi.Type == AppUserImageType.Profile)
                            .Select(pi => pi.Url)
                            .FirstOrDefault(),
                        Username = uc.AppUser.UserName
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<AppUserChat> GetChatAppUserChat(int chatId, int userId)
    {
        return await _context.AppUserChats
            .Where(x => x.AppUserId == userId && x.ChatId == chatId)
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<AppUserChat>> GetChatAppUserChats(int chatId)
    {
        return await _context.AppUserChats
            .Where(x => x.ChatId == chatId)
            .ToListAsync();
    }

    public async Task<Chat> GetChatById(int chatId, int userId)
    {
        return await _context.Chats
            .Where(x => x.Id == chatId && x.AppUserChats.Select(uc => uc.AppUserId).Contains(userId))
            .FirstOrDefaultAsync();
    }

    public ChatConnection GetChatConnectionByConnectionId(string connectionId)
    {
        return _context.ChatConnections
            .Where(x => string.Equals(x.ConnectionId, connectionId))
            .FirstOrDefault();
    }

    public async Task<ICollection<ChatPreviewDto>> GetChats(int userId, string sq)
    {
        return await _context.Chats
            .Where(x => x.AppUserChats.Select(uc => uc.AppUserId).Contains(userId) && x.Name.ToLower().Contains(sq.ToLower()))
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new ChatPreviewDto
            {
                Id = x.Id,
                IsSeen = x.AppUserChats
                    .Where(uc => uc.AppUserId == userId && uc.ChatId == x.Id)
                    .Select(uc => uc.IsSeen)
                    .FirstOrDefault(),
                Name = x.Name,
                LastMessage = x.Messages
                    .OrderByDescending(m => m.CreatedAt)
                    .Select(m => new ChatPreviewLastMessageDto
                    {
                        Content = m.Content,
                        CreatedAt = m.CreatedAt,
                        Sender = new ChatSenderDto
                        {
                            Id = m.AppUserId,
                            IsActive = m.Sender.IsActive,
                            ProfileImage = m.Sender.AppUserImages
                                .Where(pi => pi.IsDeleted == false && pi.Type == AppUserImageType.Profile)
                                .Select(pi => pi.Url)
                                .FirstOrDefault(),
                            Username = m.Sender.UserName
                        }
                    })
                    .FirstOrDefault()
            })
            .ToListAsync();
    }

    public async Task<int> GetNotSeenChatsNumber(int userId)
    {
        return await _context.AppUserChats
            .Where(x => x.AppUserId == userId && x.IsSeen == false)
            .CountAsync();
    }

    public async Task<ICollection<AppUserChat>> GetUserAppUserChats(int userId)
    {
        return await _context.AppUserChats
            .Where(x => x.AppUserId == userId)
            .ToListAsync();
    }

    public ICollection<string> GetUserChatUniqueNames(int userId)
    {
        return _context.AppUserChats
            .Where(x => x.AppUserId == userId)
            .Select(x => x.Chat.UniqueName)
            .ToList();
    }

    public async Task<string> GetUserConnectionId(int userId)
    {
        return await _context.ChatConnections
            .Where(x => x.AppUserId == userId)
            .Select(x => x.ConnectionId)
            .FirstOrDefaultAsync();
    }

    public async Task<ICollection<ChatParticipantDto>> GetUsersForChatParticipants(int userId, string sq)
    {
        return await _context.Users
            .Where(x => x.Id != userId && x.UserName.Contains(sq.ToLower()))
            .Take(10)
            .Select(x => new ChatParticipantDto
            {
                Id = x.Id,
                ProfileImage = x.AppUserImages
                    .Where(pi => pi.IsDeleted == false && pi.Type == AppUserImageType.Profile)
                    .Select(pi => pi.Url)
                    .FirstOrDefault(),
                Username = x.UserName
            })
            .ToListAsync();
    }

    public void RemoveAppUserChat(AppUserChat appUserChat)
    {
        _context.AppUserChats.Remove(appUserChat);
    }

    public void RemoveChatConnection(ChatConnection chatConnection)
    {
        _context.ChatConnections.Remove(chatConnection);
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public bool SaveAllSync()
    {
        return _context.SaveChanges() > 0;
    }
}
