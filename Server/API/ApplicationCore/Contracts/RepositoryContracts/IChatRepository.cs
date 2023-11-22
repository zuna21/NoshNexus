namespace API;

public interface IChatRepository
{
    Task<List<ChatParticipantDto>> GetUsersForChatParticipants(string likeUsername, int whoSearchId);
}
