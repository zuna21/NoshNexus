using ApplicationCore.DTOs;

namespace ApplicationCore;

public interface IChatService
{
    Task<Response<ChatDto>> Create(CreateChatDto createChatDto);
    Task<Response<ICollection<ChatParticipantDto>>> GetUsersForChatParticipants(string sq);
    Task<Response<ChatDto>> GetChat(int chatId);
    Task<Response<ICollection<ChatPreviewDto>>> GetChats(string sq);
}
