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
        var chatUniqueNames = _chatRepository.GetUserChatUniqueNamesSync(user.Id);
        List<ChatConnection> chatConnections = [];
        foreach (var chatName in chatUniqueNames)
        {
            ChatConnection chatConnection = new()
            {
                AppUserId = user.Id,
                AppUser = user,
                ConnectionId = Context.ConnectionId,
                GroupName = chatName
            };

            chatConnections.Add(chatConnection);
            await Groups.AddToGroupAsync(chatConnection.ConnectionId, chatConnection.GroupName);
        }

        _chatRepository.AddChatConnections(chatConnections);
        _chatRepository.SaveAllSync();
    }

    public async override Task OnDisconnectedAsync(Exception exception)
    {
        var chatConnections = _chatRepository.GetChatConnectionsByConnectionId(Context.ConnectionId);
        foreach (var chatConnection in chatConnections)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatConnection.GroupName);
        }

        _chatRepository.RemoveChatConnections(chatConnections);
        _chatRepository.SaveAllSync();
    }
}
