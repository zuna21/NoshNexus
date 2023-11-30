using ApplicationCore.DTOs;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IChatService
{
    Task<Response<List<ChatParticipantDto>>> GetUsersForChatParticipants(string likeUsername);
    Task<Response<ChatDto>> CreateChat(CreateChatDto createChatDto);
    Task<Response<ChatDto>> UpdateChat(int chatId, CreateChatDto createChatDto);
    Task<Response<int>> RemoveParticipant(int chatId, int participantId);
    Task<Response<ICollection<ChatPreviewDto>>> GetChats(string sqName);
    Task<Response<ChatMenuDto>> GetChatsForMenu();
    Task<Response<ChatDto>> GetChat(int id);
    Task<Response<bool>> MarkAllAsRead();
    Task<Response<MessageDto>> CreateMessage(int chatId, CreateMessageDto createMessageDto);
    Task<Response<int>> DeleteChat(int chatId);
}