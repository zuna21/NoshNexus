namespace ApplicationCore.Entities;

public class Currency
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Code { get; set; }



    // navigation properties
    public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();
}

