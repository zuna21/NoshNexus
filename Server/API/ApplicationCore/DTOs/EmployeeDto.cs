namespace API;

public class EmployeeDto
{

}

public class CreateEmployeeDto
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int RestaurantId { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public DateOnly Birth { get; set; }
    public bool CanEditMenus { get; set; }
    public bool CanViewFolders { get; set; }
    public bool CanEditFolders { get; set; }
}
