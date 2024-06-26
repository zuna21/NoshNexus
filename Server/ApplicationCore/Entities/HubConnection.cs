﻿using ApplicationCore.Entities;

namespace ApplicationCore;

public class HubConnection
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; }
    public string ConnectionId { get; set; }
    public string GroupName { get; set; }
    public HubConnectionType Type { get; set; }
}

public enum HubConnectionType
{
    Order
}
