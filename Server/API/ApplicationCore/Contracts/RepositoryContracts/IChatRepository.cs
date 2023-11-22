namespace API;

public interface IChatRepository
{
    Task<List<ChatParticipantDto>> GetUsersForChatParticipants(string likeUsername, int whoSearchId);
    Task<List<AppUser>> GetParticipantsById(ICollection<int> ids);
    
    void CreateChat(Chat chat);
    void CreateAppUserChats(List<AppUserChat> appUserChats);
    Task<bool> SaveAllAsync();
}
