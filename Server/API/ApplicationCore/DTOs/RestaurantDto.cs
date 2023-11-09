namespace API;

public class RestaurantDto
{

}

public class CreateRestaurantDto
{
    public string Name { get; set; }
    public int PostalCode { get; set; }
    public string PhoneNumber { get; set; }
    public int CountryId { get; set; }
    public int CurrencyId { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string WebsiteUrl { get; set; }
    public bool IsActive { get; set; }
}

public class GetCreateRestaurantDto
{
    public ICollection<GetCountryDto> Countries { get; set; }
    public ICollection<GetCurrencyDto> Currencies { get; set; }
}

public class GetRestaurantEditDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CountryId { get; set; }
    public int CurrencyId { get; set; }
    public int PostalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string WebsiteUrl { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public ImageDto ProfileImage { get; set; }
    public ICollection<ImageDto> Images { get; set; } = new List<ImageDto>();


    public ICollection<GetCountryDto> AllCountries { get; set; } = new List<GetCountryDto>();
    public ICollection<GetCurrencyDto> AllCurrencies { get; set; } = new List<GetCurrencyDto>();
}

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

public class RestaurantDetailsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
    public string City  { get; set; }
    public string Address { get; set; }
    public int PostalCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Description { get; set; }
    public string FacebookUrl { get; set; }
    public string InstagramUrl { get; set; }
    public string WebsiteUrl { get; set; }
    public bool IsActive { get; set; }
    public ICollection<string> RestaurantImages { get; set; }
    public int EmployeesNumber { get; set; }
    public int MenusNumber { get; set; }
    public int TodayOrdersNumber { get; set; }
}

public class RestaurantSelectDto 
{
    public int Id { get; set; }
    public string Name { get; set; }
}