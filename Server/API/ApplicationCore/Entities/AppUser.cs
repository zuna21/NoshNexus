﻿using Microsoft.AspNetCore.Identity;

namespace API;

public class AppUser : IdentityUser<int>
{

    public bool IsActive { get; set; } = false;


    // Navigation properties
    public List<Owner> Owners { get; set; } = new();
    public List<Employee> Employees { get; set; } = new();
    public List<AppUserNotification> AppUserNotifications { get; set; } = new();
}
