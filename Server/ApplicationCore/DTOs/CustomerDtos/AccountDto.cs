namespace ApplicationCore.DTOs.CustomerDtos;


public class AccountDto
{
    public string Username { get; set; }
    public string Token { get; set; }
}

public class RegisterDto
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
