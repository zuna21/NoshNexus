using ApplicationCore.DTOs.OwnerDtos;

namespace ApplicationCore.DTOs.EmployeeDtos;

public class AccountDto
{
    public string Username { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
    public string ProfileImage { get; set; }
}

public class LoginDto 
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class GetAccountDetailsDto
{
    public int Id { get; set; }
    public AccountHeaderDto AccountHeader { get; set; }
    public string Username { get; set; }
    public string FirstName  { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public string Email { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime Birth { get; set; }
    public string Restaurant { get; set; }
}


public class AccountHeaderDto
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string BackgroundImage { get; set; }
    public string ProfileImage { get; set; }
}

public class GetAccountEditDto
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City  { get; set; }
    public int CountryId { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public DateTime Birth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public ImageDto ProfileImage { get; set; }
    public ICollection<GetCountryDto> AllCountries { get; set; }
}

public class EditAccountDto
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string City { get; set; }
    public int CountryId { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public DateTime Birth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}