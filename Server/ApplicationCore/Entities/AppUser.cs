using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities;

public class AppUser : IdentityUser<int>
{

    public bool IsActive { get; set; } = false;


    // Navigation properties
    public List<Owner> Owners { get; set; } = [];
    public List<Employee> Employees { get; set; } = [];
    public List<Customer> Customers { get; set; } = [];
    public List<AppUserNotification> AppUserNotifications { get; set; } = [];
    public List<AppUserChat> AppUserChats { get; set; } = [];
    public List<Message> Messages { get; set; } = [];
    public List<AppUserImage> AppUserImages { get; set; } = [];
    public List<HubConnection> HubConnections { get; set; } = [];
    public List<ChatConnection> ChatConnections { get; set; } = [];
    public List<OrderConnection> OrderConnections { get; set; } = [];
}

