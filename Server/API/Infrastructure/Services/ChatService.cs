
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

    public async Task<Response<bool>> CreateChat(CreateChatDto createChatDto)
    {
        Response<bool> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var participants = await _chatRepository.GetParticipantsById(createChatDto.ParticipantsId);
            if (participants == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            if (participants.Count <= 0)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please select one or more users.";
                return response;
            }
            participants.Add(user);

            var chat = new Chat
            {
                Name = createChatDto.Name
            };

            _chatRepository.CreateChat(chat);
            if (!await _chatRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create chat.";
                return response;
            }

            var appUserChats = participants
                .Select(x => new AppUserChat
                {
                    AppUserId = x.Id,
                    AppUser = x,
                    ChatId = chat.Id,
                    Chat = chat
                })
                .ToList();

            _chatRepository.CreateAppUserChats(appUserChats);
            if (!await _chatRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create chat.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = true;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
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
