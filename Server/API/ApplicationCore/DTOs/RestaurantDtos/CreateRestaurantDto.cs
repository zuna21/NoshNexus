namespace API;

public class CreateRestaurantDto
{
    public string Name { get; set; }
    public int PostalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string WebsiteUrl { get; set; }
}
