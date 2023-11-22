namespace API;

public interface IChatService
{
    Task<Response<List<ChatParticipantDto>>> GetUsersForChatParticipants(string likeUsername);
}
