using ApplicationCore.DTOs;
using ApplicationCore.Entities;
using Microsoft.AspNetCore.Http;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IEmployeeService
{
    Task<Response<int>> Create(CreateEmployeeDto createEmployeeDto);
    Task<Response<int>> Update(int employeeId, EditEmployeeDto editEmployeeDto);
    Task<Response<int>> Delete(int employeeId);
    Task<Response<EmployeeAccountDto>> Login(LoginEmployeeDto loginEmployeeDto);
    Task<Response<PagedList<EmployeeCardDto>>> GetEmployees(EmployeesQueryParams employeesQueryParams);
    Task<Response<GetEmployeeEditDto>> GetEmployeeEdit(int id);
    Task<Response<EmployeeDetailsDto>> GetEmployee(int id);
    Task<Response<ImageDto>> UploadProfileImage(int employeeId, IFormFile image);

}
