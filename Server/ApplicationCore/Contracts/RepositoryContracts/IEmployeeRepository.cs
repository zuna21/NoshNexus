using ApplicationCore.Entities;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;
using EmployeeDtos = ApplicationCore.DTOs.EmployeeDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IEmployeeRepository
{
    void CreateEmployee(Employee employee);
    Task<PagedList<OwnerDtos.EmployeeCardDto>> GetEmployees(int ownerId, OwnerQueryParams.EmployeesQueryParams employeesQueryParams);
    Task<OwnerDtos.GetEmployeeEditDto> GetEmployeeEdit(int employeeId, int ownerId);
    Task<OwnerDtos.GetEmployeeDetailsDto> GetEmployee(int employeeId, int ownerId);
    Task<Employee> GetOwnerEmployee(int employeeId, int ownerId);
    Task<Employee> GetEmployeeByUsername(string username);
    Task<bool> SaveAllAsync();


    // Employee
    Task<EmployeeDtos.GetAccountDetailsDto> GetAccountDetails(int employeeId);


    // Customer
    Task<ICollection<CustomerDtos.EmployeeCardDto>> GetCustomerEmployees(int restaurantId);
    Task<CustomerDtos.EmployeeDto> GetCustomerEmployee(int employeeId);


    // For Hubs
    Employee GetEmployeeByUsernameSync(string username);
}