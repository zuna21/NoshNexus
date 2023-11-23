

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

    public async Task<Response<bool>> CreateMessage(int chatId, CreateMessageDto createMessageDto)
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

            var chat = await _chatRepository.GetChatById(chatId);
            if (chat == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var chatParticipants = await _chatRepository.GetChatParticipants(chatId);
            if (chatParticipants == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            if (chatParticipants.Count <= 0)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Add chat participants.";
                return response;
            }

            var message = new Message
            {
                AppUserId = user.Id,
                Sender = user,
                ChatId = chat.Id,
                Chat = chat,
                Content = createMessageDto.Content,
            };
            _chatRepository.CreateMessage(message);
            if (!await _chatRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to send message";
                return response;
            }

            var appUserMessages = chatParticipants
                .Select(
                    x => new AppUserMessage
                    {
                        AppUserId = x.Id,
                        AppUser = x,
                        MessageId = message.Id,
                        Message = message,
                        IsSeen = x.Id == user.Id
                    }
                )
                .ToList();
            
            _chatRepository.CreateAppUserMessages(appUserMessages);
            if (!await _chatRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create message.";
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

    public async Task<Response<ChatDto>> GetChat(int id)
    {
        Response<ChatDto> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var chat = await _chatRepository.GetChatById(id);
            if (chat == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var participants = await _chatRepository.GetChatParticipants(id);
            if (participants == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }
            
            var chatParticipants = participants.Select(x => new ChatParticipantDto
            {
                Id = x.Id,
                ProfileImage = "",
                Username = x.UserName
            })
            .ToList();

            var messages = await _chatRepository.GetChatMessages(id, user.Id);
            if (messages == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var chatDto = new ChatDto
            {
                Id = chat.Id,
                Name = chat.Name,
                Messages = messages,
                Participants = chatParticipants
            };

            response.Status = ResponseStatus.Success;
            response.Data = chatDto;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<ChatPreviewDto>>> GetChats()
    {
        Response<ICollection<ChatPreviewDto>> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var chats = await _chatRepository.GetChats(user.Id);
            if (chats == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = chats;
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
