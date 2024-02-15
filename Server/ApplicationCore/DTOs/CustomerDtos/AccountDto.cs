using ApplicationCore.DTOs.OwnerDtos;

namespace ApplicationCore.DTOs.CustomerDtos;


public class AccountDto
{
    public string Username { get; set; }
    public string Token { get; set; }
    public string ProfileImage { get; set; }
}

public class ActivateAccountDto
{
    public string Username { get; set; }
    public string Password { get; set; }
    public string RepeatPassword { get; set; }
}

public class LoginDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class GetAccountDetailsDto
{
    public int Id { get; set; }
    public string ProfileImage { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public string Country { get; set; }
    public bool IsActivated { get; set; }
    public string City { get; set; }
    public DateTime Joined { get; set; }
}

public class ProfileImageDto
{
    public int Id { get; set; }
    public string Url { get; set; }
}

public class GetAccountEditDto
{
    public int Id { get; set; }
    public ProfileImageDto ProfileImage { get; set; }
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public int CountryId { get; set; }
    public ICollection<GetCountryDto> Countries { get; set; }
    public string City { get; set; }
}

public class EditAccountDto
{
    public string Username { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Description { get; set; }
    public int CountryId { get; set;  }
    public string City { get; set; }
}