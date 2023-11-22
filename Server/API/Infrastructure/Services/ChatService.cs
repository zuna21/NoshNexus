
namespace API;

public class ChatService : IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IUserService _userService;
    public ChatService(
        IChatRepository chatRepository,
        IUserService userService
    )
    {
        _chatRepository = chatRepository;
        _userService = userService;
    }
    public async Task<Response<List<ChatParticipantDto>>> GetUsersForChatParticipants(string likeUsername)
    {
        Response<List<ChatParticipantDto>> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }
            var participants = await _chatRepository.GetUsersForChatParticipants(likeUsername, user.Id);
            if (participants == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = participants;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }
}
