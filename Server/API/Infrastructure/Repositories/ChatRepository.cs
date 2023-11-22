
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
}
