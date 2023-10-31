﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace API;

public class Employee
{
    public int Id { get; set; }
    [Required]
    public string IdentityUserId { get; set; }
    public int CountryId { get; set; }
    [Required]
    public int RestaurantId { get; set; }
    [Required]
    public string UniqueUsername { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public DateOnly Birth { get; set; }
    public bool CanEditMenus { get; set; } = false;
    public bool CanViewFolders { get; set; } = false;
    public bool CanEditFolders { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;




    // Navigation property
    public IdentityUser IdentityUser { get; set; }
    public Restaurant Restaurant { get; set; }
    public Country Country { get; set; }
}
