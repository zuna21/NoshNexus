using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IChatRepository
{
    Task<List<ChatParticipantDto>> GetUsersForChatParticipants(string likeUsername, int whoSearchId);
    Task<List<AppUser>> GetParticipantsById(ICollection<int> ids);
    Task<ICollection<ChatPreviewDto>> GetChats(int userId, string sqName);
    Task<Chat> GetChatById(int chatId, int userId);
    Task<AppUserChat> GetAppUserChat(int chatId, int userId);
    Task<ICollection<AppUserChat>> GetChatAppUsers(int chatId);
    Task<ICollection<AppUserChat>> GetAppUserChats(int userId);
    Task<ICollection<AppUser>> GetChatParticipants(int chatId);
    Task<ICollection<MessageDto>> GetChatMessages(int chatId, int userId);
    Task<int> NotSeenNumber(int userId);
    void CreateMessage(Message message);
    void CreateChat(Chat chat);
    void CreateAppUserChats(List<AppUserChat> appUserChats);
    void RemoveParticipant(AppUserChat appUserChat);
    void RemoveChat(Chat chat);
    Task<bool> SaveAllAsync();


    // Sync function for hubs
    ICollection<string> GetUserChatUniqueNamesSync(int userId);
}
