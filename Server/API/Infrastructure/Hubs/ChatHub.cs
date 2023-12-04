using Microsoft.AspNetCore.SignalR;

namespace API;

public class ChatHub : Hub
{
        public async Task SendMessageToGroup(string groupName, string message)
        {
            // Send a message to a group of users
            await Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }

        public async Task JoinGroup(string groupName)
        {
            // Add the current user to a group
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            // Remove the current user from a group
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
}
