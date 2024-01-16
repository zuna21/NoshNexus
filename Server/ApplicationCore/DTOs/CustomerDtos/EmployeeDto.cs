namespace ApplicationCore.DTOs.CustomerDtos;

public class EmployeeDto
{

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