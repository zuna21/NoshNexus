namespace ApplicationCore.DTOs.EmployeeDtos;

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
