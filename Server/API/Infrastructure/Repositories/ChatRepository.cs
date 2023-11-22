
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
}
