
using Microsoft.EntityFrameworkCore;

namespace API;

public class ChatRepository : IChatRepository
{
    private readonly DataContext _context;
    public ChatRepository(
        DataContext dataContext
    )
    {
        _context = dataContext;
    }

    public void CreateAppUserChats(List<AppUserChat> appUserChats)
    {
        _context.AppUserChats.AddRange(appUserChats);
    }

    public void CreateChat(Chat chat)
    {
        _context.Chats.Add(chat);
    }

    public async Task<ICollection<ChatPreviewDto>> GetChats(int userId)
    {
        return await _context.AppUserChats
            .Where(x => x.AppUserId == userId && x.Chat.Messages.Count > 0)
            .Select(x => new ChatPreviewDto
            {
                Id = x.ChatId,
                Name = x.Chat.Name,
                LastMessage = x
                    .Chat
                    .Messages
                    .OrderByDescending(c => c.CreatedAt)
                    .Select(c => new ChatPreviewLastMessageDto
                    {
                        Content = c.Content,
                        CreatedAt = c.CreatedAt,
                        IsSeen = c.AppUserMessages
                            .Where(m => m.MessageId == c.Id)
                            .Select(m => m.IsSeen)
                            .FirstOrDefault(),
                        Sender = new ChatSenderDto
                        {
                            Id = c.AppUserId,
                            IsActive = c.Sender.IsActive,
                            ProfileImage = "",
                            Username = c.Sender.UserName
                        }
                    })
                    .FirstOrDefault()
            })
            .ToListAsync();
    }

    public async Task<Chat> GetChatById(int chatId)
    {
        return await _context.Chats.FirstOrDefaultAsync(x => x.Id == chatId);
    }

    public async Task<List<AppUser>> GetParticipantsById(ICollection<int> ids)
    {
        return await _context.Users
            .Where(x => ids.Contains(x.Id))
            .ToListAsync();
    }

    public async Task<List<ChatParticipantDto>> GetUsersForChatParticipants(string likeUsername, int whoSearchId)
    {
        return await _context.Users
            .Where(x => x.UserName.Contains(likeUsername) && x.Id != whoSearchId)
            .Take(15)
            .Select(x => new ChatParticipantDto
            {
                Id = x.Id,
                ProfileImage = "",
                Username = x.UserName
            })
            .ToListAsync();
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }

    public void CreateMessage(Message message)
    {
        _context.Messages.Add(message);
    }

    public void CreateAppUserMessages(ICollection<AppUserMessage> appUserMessages)
    {
        _context.AppUserMessages.AddRange(appUserMessages);
    }

    public async Task<ICollection<AppUser>> GetChatParticipants(int chatId)
    {
        return await _context.AppUserChats
            .Where(x => x.ChatId == chatId)
            .Select(x => x.AppUser)
            .ToListAsync();
    }

    public async Task<ICollection<MessageDto>> GetChatMessages(int chatId, int userId)
    {
        return await _context.Messages
            .Where(x => x.ChatId == chatId)
            .Select(x => new MessageDto
            {
                Id = x.Id,
                Content = x.Content,
                CreatedAt = x.CreatedAt,
                IsMine = x.AppUserId == userId,
                Sender = new ChatSenderDto
                {
                    Id = x.AppUserId,
                    IsActive = x.Sender.IsActive,
                    ProfileImage = "",
                    Username = x.Sender.UserName
                }
            })
            .ToListAsync();
    }
}
