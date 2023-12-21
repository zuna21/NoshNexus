using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore;

public interface IChatRepository
{
    void Create(Chat chat);
    void AddChatParticipants(ICollection<AppUserChat> chatParticipants);
    void AddMessage(Message message);
    void RemoveAppUserChat(AppUserChat appUserChat);
    Task<ICollection<AppUser>> GetAppUserByIds(ICollection<int> userIds);
    Task<ChatDto> GetChat(int chatId, int userId);
    Task<ICollection<ChatParticipantDto>> GetUsersForChatParticipants(int userId, string sq);
    Task<ICollection<ChatPreviewDto>> GetChats(int userId, string sq);
    Task<int> GetNotSeenChatsNumber(int userId);
    Task<Chat> GetChatById(int chatId, int userId);

    Task<ICollection<AppUserChat>> GetUserAppUserChats(int userId);
    Task<ICollection<AppUserChat>> GetChatAppUserChats(int chatId);
    Task<AppUserChat> GetChatAppUserChat(int chatId, int userId);
    Task<bool> SaveAllAsync();


    // for hubs
    ICollection<string> GetUserChatUniqueNamesSync(int userId);
    ICollection<ChatConnection> GetChatConnectionsByConnectionId(string connectionId);
    void AddChatConnections(ICollection<ChatConnection> chatConnections);
    void RemoveChatConnections(ICollection<ChatConnection> chatConnections);
    bool SaveAllSync();
}
