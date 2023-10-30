namespace API;

public class GetRestaurantEditDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }
    public int PostalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string WebsiteUrl { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
}
