using ApplicationCore;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace API;

public class ChatService(
    IChatRepository chatRepository,
    IUserService userService
) : IChatService
{
    private readonly IChatRepository _chatRepository = chatRepository;
    private readonly IUserService _userService = userService;

    public async Task<Response<ChatDto>> Create(CreateChatDto createChatDto)
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

            Chat chat = new()
            {
                Name = createChatDto.Name,
                UniqueName = Guid.NewGuid().ToString()
            };

            if(createChatDto.ParticipantsId.Count <= 0)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please select at least one participant.";
                return response;
            }

            var users = await _chatRepository.GetAppUserByIds(createChatDto.ParticipantsId);
            if (users.Count <= 0)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Please select at least one participant.";
                return response;
            }

            _chatRepository.Create(chat);
            if (!await _chatRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to create new chat.";
                return response;
            }

            users.Add(user);
            var chatParticipants = users.Select(x => new AppUserChat
            {
                AppUserId = x.Id,
                AppUser = x,
                ChatId = chat.Id,
                Chat = chat,
                IsSeen = x.Id == user.Id
            })
            .ToList();

            _chatRepository.AddChatParticipants(chatParticipants);
            if (!await _chatRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to add chat participants";
                return response;
            }

            Message message = new()
            {
                AppUserId = user.Id,
                Sender = user,
                ChatId = chat.Id,
                Chat = chat,
                Content = $"[NoshNexus] {user.UserName} is created this chat.",
            };

            _chatRepository.AddMessage(message);
            if (!await _chatRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to add message.";
                return response;
            }

            var getChat = await _chatRepository.GetChat(chat.Id, user.Id);
            if (chat == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = getChat;


        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<int>> DeleteChat(int chatId)
    {
        Response<int> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var chatAppUserChats = await _chatRepository.GetChatAppUserChats(chatId);
            var chatAppUserChat = chatAppUserChats.FirstOrDefault(x => x.AppUserId == user.Id);
            if (chatAppUserChat == null)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to get your chat.";
                return response;
            }

            chatAppUserChats.Remove(chatAppUserChat);
            _chatRepository.RemoveAppUserChat(chatAppUserChat);

            if (!await _chatRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete chat.";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = chatAppUserChat.ChatId;

        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ChatDto>> GetChat(int chatId)
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

            var chat = await _chatRepository.GetChat(chatId, user.Id);
            if (chat == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = chat;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<ChatPreviewDto>>> GetChats(string sq)
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

            response.Status = ResponseStatus.Success;
            response.Data = await _chatRepository.GetChats(user.Id, sq);
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


            ChatMenuDto chatMenuDto = new()
            {
                Chats = await _chatRepository.GetChats(user.Id, ""),
                NotSeenNumber = await _chatRepository.GetNotSeenChatsNumber(user.Id)
            };

            response.Status = ResponseStatus.Success;
            response.Data = chatMenuDto;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ICollection<ChatParticipantDto>>> GetUsersForChatParticipants(string sq)
    {
        Response<ICollection<ChatParticipantDto>> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }


            response.Status = ResponseStatus.Success;
            response.Data = await _chatRepository.GetUsersForChatParticipants(user.Id, sq);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<bool>> MarkAllAsRead()
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

            var userChats = await _chatRepository.GetUserAppUserChats(user.Id);
            foreach (var userChat in userChats)
            {
                userChat.IsSeen = true;
            }

            await _chatRepository.SaveAllAsync();
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

    public async Task<Response<int>> RemoveParticipant(int participantId, int chatId)
    {
        Response<int> response = new();
        try
        {
            var user = await _userService.GetUser();
            if (user == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var chatAppUserChat = await _chatRepository.GetChatAppUserChat(chatId, participantId);
            if (chatAppUserChat == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            _chatRepository.RemoveAppUserChat(chatAppUserChat);
            if (!await _chatRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to remove user";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = chatAppUserChat.AppUserId;

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
