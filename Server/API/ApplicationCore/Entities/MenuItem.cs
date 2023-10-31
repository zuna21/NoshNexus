﻿namespace API;

public class MenuItem
{
    public int Id { get; set; }
    public int MenuId  { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; } = 0;
    public bool IsActive { get; set; } = false;
    public bool HasSpecialOffer { get; set; } = false;
    public double SpecialOfferPrice { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    


    // Navigation properties
    public Menu Menu { get; set; }
}