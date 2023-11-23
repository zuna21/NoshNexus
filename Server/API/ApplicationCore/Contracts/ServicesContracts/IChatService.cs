namespace API;

public interface IChatService
{
    Task<Response<List<ChatParticipantDto>>> GetUsersForChatParticipants(string likeUsername);
    Task<Response<bool>> CreateChat(CreateChatDto createChatDto);
    Task<Response<ICollection<ChatPreviewDto>>> GetChats();
    Task<Response<ChatDto>> GetChat(int id);
    Task<Response<bool>> CreateMessage(int chatId, CreateMessageDto createMessageDto);
}
