namespace API;

public class RestaurantCardDto
{
    public int Id { get; set; }
    public string ProfileImage { get; set; }
    public string Name { get; set; }
    public bool IsOpen { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
}
