namespace API;

public interface IChatRepository
{
    Task<List<ChatParticipantDto>> GetUsersForChatParticipants(string likeUsername, int whoSearchId);
    Task<List<AppUser>> GetParticipantsById(ICollection<int> ids);
    Task<ICollection<ChatPreviewDto>> GetChats(int userId);
    Task<Chat> GetChatById(int chatId);
    Task<ICollection<AppUser>> GetChatParticipants(int chatId);
    void CreateMessage(Message message);
    void CreateChat(Chat chat);
    void CreateAppUserMessages(ICollection<AppUserMessage> appUserMessages);
    void CreateAppUserChats(List<AppUserChat> appUserChats);
    Task<bool> SaveAllAsync();
}
