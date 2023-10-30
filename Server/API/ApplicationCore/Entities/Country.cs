namespace API;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }



    // Navigation properties
    public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
    public List<Owner> Owners { get; set; } = new List<Owner>();
}
