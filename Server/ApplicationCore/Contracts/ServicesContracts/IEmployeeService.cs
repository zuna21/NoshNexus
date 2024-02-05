using ApplicationCore.DTOs;

using OwnerDtos = ApplicationCore.DTOs.OwnerDtos;
using EmployeeDtos = ApplicationCore.DTOs.EmployeeDtos;
using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

using OwnerQueryParams = ApplicationCore.QueryParams.OwnerQueryParams;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IEmployeeService
{
    Task<Response<int>> Create(OwnerDtos.CreateEmployeeDto createEmployeeDto);
    Task<Response<int>> Update(int employeeId, OwnerDtos.EditEmployeeDto editEmployeeDto);
    Task<Response<int>> Delete(int employeeId);
    Task<Response<EmployeeDtos.AccountDto>> Login(EmployeeDtos.LoginDto loginEmployeeDto);
    Task<Response<PagedList<OwnerDtos.EmployeeCardDto>>> GetEmployees(OwnerQueryParams.EmployeesQueryParams employeesQueryParams);
    Task<Response<OwnerDtos.GetEmployeeEditDto>> GetEmployeeEdit(int id);
    Task<Response<OwnerDtos.GetEmployeeDetailsDto>> GetEmployee(int id);


    // Employee
    Task<Response<EmployeeDtos.GetAccountDetailsDto>> GetAccountDetails();
    Task<Response<EmployeeDtos.GetAccountEditDto>> GetAccountEdit();
    Task<Response<EmployeeDtos.AccountDto>> EditAccount(EmployeeDtos.EditAccountDto editAccountDto);


    // Customer
    Task<Response<ICollection<CustomerDtos.EmployeeCardDto>>> GetCustomerEmployees(int restaurantId);
    Task<Response<CustomerDtos.EmployeeDto>> GetCustomerEmployee(int employeeId);
}
