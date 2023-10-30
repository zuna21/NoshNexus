using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API;

public class DataContext : IdentityDbContext
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
}
