namespace API;

public interface ICustomerService
{
    Task<Response<CustomerDto>> Register(RegisterCustomerDto registerCustomerDto);    
    Task<Response<CustomerDto>> Login(LoginCustomerDto loginCustomerDto);


    Task<Customer> GetCustomer();
}