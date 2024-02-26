using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Country
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Code { get; set; }



    // Navigation properties
    public List<Restaurant> Restaurants { get; set; } = [];
    public List<Owner> Owners { get; set; } = [];
    public List<Employee> Employees { get; set; } = [];
    public List<Customer> Customers { get; set; } = [];
}
