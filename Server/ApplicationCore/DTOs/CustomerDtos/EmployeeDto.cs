namespace ApplicationCore.DTOs.CustomerDtos;

public class EmployeeDto
{
    public int Id { get; set; }
    public string RestaurantImage { get; set; }
    public string ProfileImage { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public string City { get; set; }
    public DateTime Birth { get; set; }
    public string Country { get; set; }
}

public class EmployeeCardDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Description { get; set; }
    public string ProfileImage { get; set; }
    public EmployeeRestaurantDto Restaurant { get; set; }
}

public class EmployeeRestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProfileImage { get; set; }
}