﻿namespace API;

public class CustomerDto
{
    public string Username { get; set; }
    public string Token { get; set; }
}

public class RegisterCustomerDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginCustomerDto
{
    public string Username { get; set; }
    public string Password { get; set; }
}
