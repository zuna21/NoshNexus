using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore;

public interface IChatRepository
{
    void Create(Chat chat);
    void AddChatParticipants(ICollection<AppUserChat> chatParticipants);
    void AddMessage(Message message);
    Task<ICollection<AppUser>> GetAppUserByIds(ICollection<int> userIds);
    Task<ChatDto> GetChat(int chatId, int userId);
    Task<ICollection<ChatParticipantDto>> GetUsersForChatParticipants(int userId, string sq);

    Task<bool> SaveAllAsync();
}
