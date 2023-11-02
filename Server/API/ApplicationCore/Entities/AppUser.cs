using Microsoft.AspNetCore.Identity;

namespace API;

public class AppUser : IdentityUser<int>
{


    // Navigation properties
    public List<Owner> Owners { get; set; } = new List<Owner>();
    public List<Employee> Employees { get; set; } = new List<Employee>();
}
