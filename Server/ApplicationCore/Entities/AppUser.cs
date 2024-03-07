using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities;

public class AppUser : IdentityUser<int>
{

    public bool IsActive { get; set; } = false;
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiryTime { get; set; }
    public string FcmToken { get; set; }

    


    // Navigation properties
    public List<Owner> Owners { get; set; } = [];
    public List<Employee> Employees { get; set; } = [];
    public List<Customer> Customers { get; set; } = [];
    public List<AppUserNotification> AppUserNotifications { get; set; } = [];
    public List<AppUserImage> AppUserImages { get; set; } = [];
    public List<HubConnection> HubConnections { get; set; } = [];
    public List<OrderConnection> OrderConnections { get; set; } = [];
}

