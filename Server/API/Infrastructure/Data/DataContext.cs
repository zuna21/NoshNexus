﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API;

public class DataContext : IdentityDbContext<AppUser, AppRole, int>
{
    public DataContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
    }


    public DbSet<Owner> Owners { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Table> Tables { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<MenuItem> MenuItems { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Currency> Currencies { get; set; }
    public DbSet<RestaurantImage> RestaurantImages { get; set; }
    public DbSet<EmployeeImage> EmployeeImages { get; set; }
    public DbSet<MenuItemImage> MenuItemImages { get; set; }
    public DbSet<OwnerImage> OwnerImages { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<AppUserNotification> AppUserNotifications { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<AppUserChat> AppUserChats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<AppUserImage> AppUserImages { get; set; }
}
