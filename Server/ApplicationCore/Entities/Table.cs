﻿namespace ApplicationCore.Entities;

public class Table
{
    public int Id { get; set; }
    public int RestaurantId { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Restaurant Restaurant { get; set; }
    public List<Order> Orders { get; set; } = new();
}
