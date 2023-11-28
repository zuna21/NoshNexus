﻿namespace API;

public interface ICustomerService
{
    Task<Response<CustomerDto>> Register(RegisterCustomerDto registerCustomerDto);    

    Task<Customer> GetCustomer();
}