using ApplicationCore.Entities;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface ICustomerRepository
{
    void Create(Customer customer);
    Task<Customer> GetCustomerByUsername(string username);
    Task<Customer> GetCustomerById(int id);
    Task<bool> SaveAllAsync();



    // Customer
    Task<CustomerDtos.GetAccountDetailsDto> GetAccountDetails(int customerId);
    Task<CustomerDtos.GetAccountEditDto> GetAccountEdit(int customerId);


}

