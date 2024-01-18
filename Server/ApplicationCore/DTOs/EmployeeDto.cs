using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;

namespace ApplicationCore.DTOs;

public class EmployeeDto
{

}

public class EmployeeAccountDto
{
    public string Username { get; set; }
    public string Token { get; set; }
}

public class LoginEmployeeDto 
{
    public string Username { get; set; }
    public string Password { get; set; }
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
    public DateTime Birth { get; set; }
    public bool CanEditMenus { get; set; }
    public bool CanViewFolders { get; set; }
    public bool CanEditFolders { get; set; }
}

public class EditEmployeeDto 
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public int RestaurantId { get; set; }
    public DateTime Birth { get; set; }
    public string Description { get; set; }
    public bool CanEditMenus { get; set; }
    public bool CanViewFolders { get; set; }
    public bool CanEditFolders { get; set; }
}

public class EmployeeCardDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
    public string Description { get; set; }
    public string ProfileImage { get; set; }
    public EmployeeCardRestaurantDto Restaurant { get; set; }
}

public class EmployeeCardRestaurantDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProfileImage { get; set; }
}

public class GetEmployeeEditDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string PhoneNumber { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public ICollection<OwnerDtos.GetRestaurantForSelectDto> OwnerRestaurants { get; set; }
    public int RestaurantId { get; set; } // Nema potrebe za objektom
    public DateTime Birth { get; set; }
    public string Description { get; set; }
    public bool CanEditMenus { get; set; }
    public ImageDto ProfileImage { get; set; }
    public bool CanEditFolders { get; set; }
    public bool CanViewFolders { get; set; }
    
}


public class EmployeeDetailsDto 
{
    public int Id { get; set; }
    public ProfileHeaderDto ProfileHeader { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Birth { get; set; }
    public bool CanEditMenus { get; set; }
    public bool CanEditFolders { get; set; }
    public bool CanViewFolders { get; set; }
    public string Restaurant { get; set; }
}
