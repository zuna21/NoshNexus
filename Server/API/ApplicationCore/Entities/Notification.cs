﻿namespace API;

public class Notification
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsDeleted { get; set; } = false;
    public DateTime CretaedAt { get; set; } = DateTime.UtcNow;


    // navigation properties
    public List<AppUserNotification> AppUserNotifications { get; set; } = new();
}
