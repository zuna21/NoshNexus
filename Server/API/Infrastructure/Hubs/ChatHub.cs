using System.Text.RegularExpressions;
using ApplicationCore;
using ApplicationCore.Contracts.RepositoryContracts;
using Microsoft.AspNetCore.SignalR;

namespace API;

public class ChatHub(
    IAppUserRepository appUserRepository,
    IChatRepository chatRepository
) : Hub
{
    private readonly IAppUserRepository _appUserRepository = appUserRepository;
    private readonly IChatRepository _chatRepository = chatRepository;

    public async override Task OnConnectedAsync()
    {
        var username = Context.UserIdentifier;
        var user = _appUserRepository.GetUserByUsernameSync(username);
        if (user == null) return;
        ChatConnection chatConnection = new() 
        {
            AppUserId = user.Id,
            AppUser = user,
            ConnectionId = Context.ConnectionId
        };

        _chatRepository.AddChatConnection(chatConnection);
        if (_chatRepository.SaveAllSync()) return;

        var chatUniqueNames = _chatRepository.GetUserChatUniqueNames(user.Id);
        foreach (var chatName in chatUniqueNames)
        {
            await Groups.AddToGroupAsync(chatConnection.ConnectionId, chatName);
        }
    }

    public async override Task OnDisconnectedAsync(Exception exception)
    {
        var username = Context.UserIdentifier;
        var user = _appUserRepository.GetUserByUsernameSync(username);
        if (user == null) return;
        var chatUniqueNames = _chatRepository.GetUserChatUniqueNames(user.Id);
        foreach (var chatUniqueName in chatUniqueNames)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatUniqueName);
        }

        var chatConnection = _chatRepository.GetChatConnectionByConnectionId(Context.ConnectionId);
        if (chatConnection == null) return;
        _chatRepository.RemoveChatConnection(chatConnection);
        _chatRepository.SaveAllSync();
    }
}
