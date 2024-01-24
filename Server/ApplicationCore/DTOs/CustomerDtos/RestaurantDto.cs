namespace ApplicationCore.DTOs.CustomerDtos;

public class RestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public int PostalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string WebsiteUrl { get; set; }
    public bool IsOpen { get; set; }
    public ICollection<string> RestaurantImages { get; set; }
    public int EmployeesNumber { get; set; }
    public int MenusNumber { get; set; }
    public bool IsFavourite { get; set; }
}
