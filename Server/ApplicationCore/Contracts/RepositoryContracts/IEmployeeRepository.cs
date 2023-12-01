using ApplicationCore.DTOs;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IEmployeeRepository
{
    void CreateEmployee(Employee employee);
    Task<ICollection<EmployeeCardDto>> GetEmployees(int ownerId);
    Task<GetEmployeeEditDto> GetEmployeeEdit(int employeeId, int ownerId);
    Task<EmployeeDetailsDto> GetEmployee(int employeeId, int ownerId);
    Task<Employee> GetOwnerEmployee(int employeeId, int ownerId);
    Task<Employee> GetEmployeeByUsername(string username);
    Task<bool> SaveAllAsync();
}