using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.ServicesContracts;

public interface IEmployeeService
{
    Task<Response<int>> Create(CreateEmployeeDto createEmployeeDto);
    Task<Response<int>> Update(int employeeId, EditEmployeeDto editEmployeeDto);
    Task<Response<int>> Delete(int employeeId);
    Task<Response<ICollection<EmployeeCardDto>>> GetEmployees();
    Task<Response<GetEmployeeEditDto>> GetEmployeeEdit(int id);
    Task<Response<EmployeeDetailsDto>> GetEmployee(int id);

    // Globalna funkcija
    Task<Employee> GetOwnerEmployee(int employeeId);
}
