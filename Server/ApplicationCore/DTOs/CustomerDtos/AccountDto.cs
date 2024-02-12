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
