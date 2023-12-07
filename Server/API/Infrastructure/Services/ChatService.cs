using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Contracts.ServicesContracts;
using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.IdentityModel.Tokens;

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
                Name = createChatDto.Name,
                UniqueName = Guid.NewGuid().ToString()
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

            var chat = await _chatRepository.GetChatById(chatId, user.Id);
            if (chat == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var appUserChat = await _chatRepository.GetAppUserChat(chatId, user.Id);
            if (appUserChat == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            _chatRepository.RemoveParticipant(appUserChat);
            if (!await _chatRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to delete chat.";
                return response;
            }

            var chatParticipants = await _chatRepository.GetChatAppUsers(chat.Id);
            if (chatParticipants == null || chatParticipants.Count <= 0)
            {
                _chatRepository.RemoveChat(chat);
                if (!await _chatRepository.SaveAllAsync())
                {
                    response.Status = ResponseStatus.BadRequest;
                    response.Message = "Failed to delete chat.";
                    return response;
                }
            }

            response.Status = ResponseStatus.Success;
            response.Data = chat.Id;
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

    public async Task<Response<ICollection<ChatPreviewDto>>> GetChats(string sqName)
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

            var chats = await _chatRepository.GetChats(user.Id, sqName);
            var orderedChats = chats.OrderByDescending(x => x.LastMessage.CreatedAt).ToList();
            if (chats == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = orderedChats;
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

            var chats = await _chatRepository.GetChats(user.Id, "");
            var orderedChats = chats.OrderByDescending(x => x.LastMessage.CreatedAt).ToList();
            if (chats == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var notSeenNumber = await _chatRepository.NotSeenNumber(user.Id);
            var chatMenu = new ChatMenuDto
            {
                Chats = orderedChats,
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

            var appUserChats = await _chatRepository.GetAppUserChats(user.Id);
            if (appUserChats == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            foreach(var appUserChat in appUserChats)
            {
                appUserChat.IsSeen = true;
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

    public async Task<Response<int>> RemoveParticipant(int chatId, int participantId)
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

            if (user.Id == participantId)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "You can't remove yourself. Try to delete chat instead.";
                return response;
            }

            var chatParticipant = await _chatRepository.GetAppUserChat(chatId, participantId);
            if (chatParticipant == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            _chatRepository.RemoveParticipant(chatParticipant);
            if (!await _chatRepository.SaveAllAsync())
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to remove participant";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = chatParticipant.AppUserId;
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
            response.Status = ResponseStatus.BadRequest;
            response.Message = "Something went wrong.";
        }

        return response;
    }

    public async Task<Response<ChatDto>> UpdateChat(int chatId, CreateChatDto createChatDto)
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

            var chat = await _chatRepository.GetChatById(chatId, user.Id);
            if (chat == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            if (chat.Name != createChatDto.Name && !createChatDto.Name.IsNullOrEmpty())
            {
                chat.Name = createChatDto.Name;
                await _chatRepository.SaveAllAsync();
            }

            var chatParticipants = await _chatRepository.GetChatParticipants(chat.Id);
            if (chatParticipants == null)
            {
                response.Status = ResponseStatus.NotFound;
                return response;
            }

            var chatParticipantsIds = chatParticipants.Select(x => x.Id).ToList();
            List<int> newUsersIds = new();
            foreach (var newUserId in createChatDto.ParticipantsId)
            {
                if(!chatParticipantsIds.Contains(newUserId)) newUsersIds.Add(newUserId);
            }

            if (newUsersIds.Count > 0)
            {
                var newChatParticipants = await _chatRepository.GetParticipantsById(newUsersIds);
                if (newChatParticipants == null)
                {
                    response.Status = ResponseStatus.NotFound;
                    return response;
                }

                var newAppUserChats = newChatParticipants.Select(x => new AppUserChat
                {
                    AppUserId = x.Id,
                    AppUser = x,
                    ChatId = chat.Id,
                    Chat = chat,
                    IsSeen = false
                }).ToList();

                _chatRepository.CreateAppUserChats(newAppUserChats);
                await _chatRepository.SaveAllAsync();
            }

            var allChatParticipants = await _chatRepository.GetChatParticipants(chat.Id);
            var allChatParticipantsDto = allChatParticipants.Select(x => new ChatParticipantDto
            {
                Id = x.Id,
                ProfileImage = "",
                Username = x.UserName
            }).ToList();
            if (allChatParticipants == null || allChatParticipants.Count <= 0)
            {
                response.Status = ResponseStatus.BadRequest;
                response.Message = "Failed to get chat participants";
                return response;
            }

            response.Status = ResponseStatus.Success;
            response.Data = new ChatDto
            {
                Id = chat.Id,
                Name = chat.Name,
                Participants = allChatParticipantsDto
            };
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
