namespace ApplicationCore.Entities;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }



    // Navigation properties
    public List<Restaurant> Restaurants { get; set; } = [];
    public List<Owner> Owners { get; set; } = [];
    public List<Employee> Employees { get; set; } = [];
    public List<Customer> Customers { get; set; } = [];
}
