namespace API;

public interface IChatService
{
    Task<Response<List<ChatParticipantDto>>> GetUsersForChatParticipants(string likeUsername);
    Task<Response<ChatDto>> CreateChat(CreateChatDto createChatDto);
    Task<Response<ICollection<ChatPreviewDto>>> GetChats();
    Task<Response<ChatMenuDto>> GetChatsForMenu();
    Task<Response<ChatDto>> GetChat(int id);
    Task<Response<bool>> MarkAllAsRead();
    Task<Response<MessageDto>> CreateMessage(int chatId, CreateMessageDto createMessageDto);
}
