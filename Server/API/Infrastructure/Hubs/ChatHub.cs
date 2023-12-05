using ApplicationCore.Contracts.RepositoryContracts;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.SignalR;

namespace API;

public class ChatHub : Hub
{
        private readonly IAppUserRepository _appUserRepository;
        private readonly IChatRepository _chatRepository;
        public ChatHub(
            IAppUserRepository appUserRepository,
            IChatRepository chatRepository
        )
        {
            _appUserRepository = appUserRepository;
            _chatRepository = chatRepository;
        }
        
        public async Task SendMessageToGroup(string groupName, string message)
        {
            // Send a message to a group of users
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }

        public async Task JoinGroups(string username)
        {
            AppUser user = _appUserRepository.GetUserByUsernameSync(username);
            if (user == null)
            {
                return;
            }

            var chats = _chatRepository.GetUserChatUniqueNamesSync(user.Id);
            foreach (var chat in chats)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, chat);
            }
        }

        public async Task LeaveGroup(string groupName)
        {
            // Remove the current user from a group
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
}
