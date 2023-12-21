﻿using Microsoft.AspNetCore.Identity;

namespace ApplicationCore.Entities;

public class AppUser : IdentityUser<int>
{

    public bool IsActive { get; set; } = false;


    // Navigation properties
    public List<Owner> Owners { get; set; } = new();
    public List<Employee> Employees { get; set; } = new();
    public List<Customer> Customers { get; set; } = new();
    public List<AppUserNotification> AppUserNotifications { get; set; } = new();
    public List<AppUserChat> AppUserChats { get; set; } = new();
    public List<Message> Messages { get; set; } = new();
    public List<AppUserImage> AppUserImages { get; set; } = [];
    public List<HubConnection> HubConnections { get; set; } = [];
    public List<ChatConnection> ChatConnections { get; set; } = [];
}

