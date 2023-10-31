namespace API;

public class OwnerDto
{

}

public class LoginOwnerDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class OwnerAccountDto
{
    public string Username { get; set; }
    public string Token { get; set; }
}


public class RegisterOwnerDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public int CountryId { get; set; }
    public string Password { get; set; }
}
