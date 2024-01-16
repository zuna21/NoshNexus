using ApplicationCore.DTOs;
using ApplicationCore.Entities;

using CustomerDtos = ApplicationCore.DTOs.CustomerDtos;

namespace ApplicationCore.Contracts.RepositoryContracts;

public interface IEmployeeRepository
{
    void CreateEmployee(Employee employee);
    Task<PagedList<EmployeeCardDto>> GetEmployees(int ownerId, EmployeesQueryParams employeesQueryParams);
    Task<GetEmployeeEditDto> GetEmployeeEdit(int employeeId, int ownerId);
    Task<EmployeeDetailsDto> GetEmployee(int employeeId, int ownerId);
    Task<Employee> GetOwnerEmployee(int employeeId, int ownerId);
    Task<Employee> GetEmployeeByUsername(string username);
    Task<bool> SaveAllAsync();


    // Customer
    Task<ICollection<CustomerDtos.EmployeeCardDto>> GetCustomerEmployees(int restaurantId);


    // For Hubs
    Employee GetEmployeeByUsernameSync(string username);
}