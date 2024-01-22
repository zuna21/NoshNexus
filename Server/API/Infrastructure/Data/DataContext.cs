using ApplicationCore;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
    public DbSet<MenuItemImage> MenuItemImages { get; set; }
    public DbSet<OwnerImage> OwnerImages { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<AppUserNotification> AppUserNotifications { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<AppUserChat> AppUserChats { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<AppUserImage> AppUserImages { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderMenuItem> OrderMenuItems { get; set; }
    public DbSet<HubConnection> HubConnections { get; set; }
    public DbSet<ChatConnection> ChatConnections { get; set; }
    public DbSet<RestaurantBlockedCustomers> RestaurantBlockedCustomers { get; set; }
    public DbSet<FavouriteCustomerRestaurant> FavouriteCustomerRestaurants { get; set; }
    public DbSet<FavouriteCustomerMenuItem> FavouriteCustomerMenuItems { get; set; }
    public DbSet<OrderConnection> OrderConnections { get; set; }
    public DbSet<RestaurantReview> RestaurantReviews { get; set; }
}
