

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

    public async Task<Response<ChatDto>> CreateChat(CreateChatDto createChatDto)
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
                    Chat = chat,
                    IsSeen = x.Id == user.Id
                })
                .ToList();

            _chatRepository.CreateAppUserChats(appUserChats);
            if (!await _chatRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create chat.";
                return response;
            }

            var chatParticipants = participants.Select(x => new ChatParticipantDto
            {
                Id = x.Id,
                ProfileImage = "",
                Username = x.UserName
            })
            .ToList();

            var message = new Message
            {
                AppUserId = user.Id,
                Sender = user,
                Chat = chat,
                ChatId = chat.Id,
                Content = $"[Nosh Nexus] {user.UserName} is create this group.",
            };

            _chatRepository.CreateMessage(message);
            if (!await _chatRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create first message.";
                return response;
            }

            var messageDto = new MessageDto
            {
                IsMine = true,
                Content = message.Content,
                CreatedAt = message.CreatedAt,
                Id = message.Id,
                Sender = new ChatSenderDto
                {
                    Id = user.Id,
                    IsActive = user.IsActive,
                    ProfileImage = "",
                    Username = user.UserName
                }
            };
            List<MessageDto> messages = new()
            {
                messageDto
            };

            var chatDto = new ChatDto
            {
                Id = chat.Id,
                Name = chat.Name,
                Participants = chatParticipants,
                Messages = messages
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

    public async Task<Response<MessageDto>> CreateMessage(int chatId, CreateMessageDto createMessageDto)
    {
        Response<MessageDto> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var chat = await _chatRepository.GetChatById(chatId, user.Id);
            if (chat == null)
            {
                response.Status = ResponseStatus.NotFound;
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

            var appUserChats = await _chatRepository.GetAppUserChats(chat.Id);
            if (appUserChats == null || appUserChats.Count <= 0)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            foreach (var appUserChat in appUserChats)
            {
                if(appUserChat.AppUserId == user.Id) appUserChat.IsSeen = true;
                else appUserChat.IsSeen = false;
            }

            await _chatRepository.SaveAllAsync();

            var messageDto = new MessageDto
            {
                Id = message.Id,
                Content = message.Content,
                Sender = new ChatSenderDto
                {
                    Id = user.Id,
                    IsActive = user.IsActive,
                    ProfileImage = "",
                    Username = user.UserName
                },
                IsMine = true,
                CreatedAt = message.CreatedAt
            };

            response.Status = ResponseStatus.Success;
            response.Data = messageDto;
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

            var chat = await _chatRepository.GetChatById(id, user.Id);
            if (chat == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var appUserChat = await _chatRepository.GetAppUserChat(id, user.Id);
            if (appUserChat == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            };

            if (appUserChat.IsSeen == false)
            {
                appUserChat.IsSeen = true;
                if (!await _chatRepository.SaveAllAsync())
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Failed to mark chat as read.";
                    return response;
                }
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

    public async Task<Response<ChatMenuDto>> GetChatsForMenu()
    {
        Response<ChatMenuDto> response = new();
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

            var notSeenNumber = await _chatRepository.NotSeenNumber(user.Id);
            var chatMenu = new ChatMenuDto
            {
                Chats = chats,
                NotSeenNumber = notSeenNumber
            };

            response.Status = ResponseStatus.Success;
            response.Data = chatMenu;
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
