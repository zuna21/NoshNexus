namespace ApplicationCore.DTOs.OwnerDtos;

public class AccountDto
{
    public string Username { get; set; }
    public string Token { get; set; }
}

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class RegisterDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int CountryId { get; set; }
    public string Password { get; set; }
}

public class GetAccountEditDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Birth { get; set; }
    public int CountryId { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public ImageDto ProfileImage { get; set; }
    public ICollection<GetCountryDto> AllCountries { get; set; }
}

public class EditAccountDto
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Birth { get; set; }
    public int CountryId { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
}

public class AccountProfileHeaderDto
{
    public string ProfileImage { get; set ;}
    public string BackgroundImage { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Username { get; set; }
}

public class GetAccountDetailsDto
{
    public int Id { get; set; }
    public AccountProfileHeaderDto ProfileHeader { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Birth { get; set; }
    public int EmployeesNumber { get; set; }
    public int RestaurantsNumber { get; set; }
    public int MenusNumber { get; set; }
    public int TodayOrdersNumber { get; set; }
}